using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using netDxf;
using netDxf.Entities;
using netDxf.Tables;

namespace EtabsTools
{
    public static class KolonDwgExporter
    {
        // --- SABITLER (ZWCAD kodundan birebir) ---
        private const double SabitGorselYaricap = 1.75;
        private const double Paspayi = 3.5;
        private const double FixedBoxWidth = 200.0;
        private const double FixedBoxHeight = 400.0;
        private const double PlanScale = 100.0; // ETABS m -> DXF cm

        private static readonly short[] OncelikliRenkler = { 1, 2, 3, 4, 6, 7, 5, 30, 150, 190, 11, 68, 144, 171, 240, 115, 225, 53 };

        // --- ANA EXPORT FONKSİYONU ---
        public static void ExportToDxf(
            string filePath,
            List<ColumnRebarData> allColumns,
            List<BeamData> beams,
            List<WallData> walls,
            Dictionary<string, Color> rebarColors,
            string currentStory,
            List<string> storyOrder)
        {
            DxfDocument dxf = new DxfDocument();

            // ZWCAD kodundaki gibi tek layer: DONE-AS
            Layer layerDone = new Layer("DONE-AS");
            layerDone.Color = new AciColor(4); // Cyan

            // Sadece mevcut katın kolonları (plan için)
            var storyColumns = allColumns.Where(c => c.Story == currentStory).ToList();

            // Tüm katlardaki kolonlardan tip gruplama
            var tipGruplari = storyColumns
                .GroupBy(c => c.TypeLabel ?? "Bilinmiyor")
                .OrderBy(g => g.Key)
                .ToList();

            // Her tipe OncelikliRenkler'den renk ata
            var tipRenkMap = new Dictionary<string, short>();
            int renkIdx = 0;
            foreach (var g in tipGruplari)
            {
                short renk = (renkIdx < OncelikliRenkler.Length) ? OncelikliRenkler[renkIdx] : (short)(31 + renkIdx);
                tipRenkMap[g.Key] = renk;
                renkIdx++;
                if (renkIdx >= OncelikliRenkler.Length) renkIdx = 0;
            }

            // ============================
            // 1. KAT PLANI ÇİZİMİ
            // ============================
            DrawPlanView(dxf, storyColumns, beams, walls, tipRenkMap, currentStory, layerDone);

            // ============================
            // 2. KOLON TİP DETAY ÇİZİMLERİ (daralma destekli)
            // ============================
            DrawColumnTypeDetails(dxf, allColumns, storyColumns, tipGruplari, tipRenkMap, storyOrder, layerDone);

            // Kaydet
            dxf.Save(filePath);
        }

        // ============================
        // KAT PLANI
        // ============================
        private static void DrawPlanView(
            DxfDocument dxf,
            List<ColumnRebarData> columns,
            List<BeamData> beams,
            List<WallData> walls,
            Dictionary<string, short> tipRenkMap,
            string storyName,
            Layer layer)
        {
            double planOffsetX = 0;
            double planOffsetY = 0;

            // Plan başlığı
            MText planTitle = new MText(
                $"KAT PLANI - {storyName}",
                new Vector2(planOffsetX, planOffsetY + 50), 5.0);
            planTitle.Layer = layer;
            planTitle.Color = new AciColor(4); // Cyan
            dxf.Entities.Add(planTitle);

            // Kirişler
            foreach (var beam in beams)
            {
                double x1 = beam.X1 * PlanScale + planOffsetX;
                double y1 = beam.Y1 * PlanScale + planOffsetY;
                double x2 = beam.X2 * PlanScale + planOffsetX;
                double y2 = beam.Y2 * PlanScale + planOffsetY;

                Line line = new Line(new Vector2(x1, y1), new Vector2(x2, y2));
                line.Layer = layer;
                line.Color = new AciColor(3); // Green
                dxf.Entities.Add(line);
            }

            // Perdeler
            foreach (var wall in walls)
            {
                if (wall.Points == null || wall.Points.Count < 2) continue;

                double x1 = wall.Points[0].X * PlanScale + planOffsetX;
                double y1 = wall.Points[0].Y * PlanScale + planOffsetY;
                double x2 = wall.Points[wall.Points.Count - 1].X * PlanScale + planOffsetX;
                double y2 = wall.Points[wall.Points.Count - 1].Y * PlanScale + planOffsetY;

                double thick = wall.Thickness * PlanScale;
                double dx = x2 - x1;
                double dy = y2 - y1;
                double len = Math.Sqrt(dx * dx + dy * dy);
                if (len < 0.001) continue;

                double nx = -dy / len * thick / 2;
                double ny = dx / len * thick / 2;

                var wallPoly = new Polyline2D(new List<Polyline2DVertex>
                {
                    new Polyline2DVertex(x1 + nx, y1 + ny),
                    new Polyline2DVertex(x2 + nx, y2 + ny),
                    new Polyline2DVertex(x2 - nx, y2 - ny),
                    new Polyline2DVertex(x1 - nx, y1 - ny)
                }, true);
                wallPoly.Layer = layer;
                wallPoly.Color = new AciColor(4); // Cyan
                dxf.Entities.Add(wallPoly);
            }

            // Kolonlar
            foreach (var col in columns)
            {
                double cx = col.X * PlanScale + planOffsetX;
                double cy = col.Y * PlanScale + planOffsetY;
                double w = col.Width * PlanScale;
                double d = col.Depth * PlanScale;
                double angle = col.Angle;

                double hw = w / 2.0;
                double hd = d / 2.0;
                double rad = angle * Math.PI / 180.0;
                double cosA = Math.Cos(rad);
                double sinA = Math.Sin(rad);

                // Kolon dikdörtgeni köşeleri
                var c0 = Rotate(cx, cy, -hw, -hd, cosA, sinA);
                var c1 = Rotate(cx, cy, hw, -hd, cosA, sinA);
                var c2 = Rotate(cx, cy, hw, hd, cosA, sinA);
                var c3 = Rotate(cx, cy, -hw, hd, cosA, sinA);

                // Kolonun tip rengini al
                string tipLabel = col.TypeLabel ?? "Bilinmiyor";
                short colRenk = tipRenkMap.ContainsKey(tipLabel) ? tipRenkMap[tipLabel] : (short)7;

                // 1. DIŞ İŞARET DİKDÖRTGENİ (ZWCAD Isaretle: ext ±15, ConstantWidth=5.0)
                // Plan ölçeğinde padding (1.5 cm her yöne)
                double pad = 15;
                double mHw = hw + pad;
                double mHd = hd + pad;
                var m0 = Rotate(cx, cy, -mHw, -mHd, cosA, sinA);
                var m1 = Rotate(cx, cy, mHw, -mHd, cosA, sinA);
                var m2 = Rotate(cx, cy, mHw, mHd, cosA, sinA);
                var m3 = Rotate(cx, cy, -mHw, mHd, cosA, sinA);

                var markPoly = new Polyline2D(new List<Polyline2DVertex>
                {
                    new Polyline2DVertex(m0.X, m0.Y),
                    new Polyline2DVertex(m1.X, m1.Y),
                    new Polyline2DVertex(m2.X, m2.Y),
                    new Polyline2DVertex(m3.X, m3.Y)
                }, true);
                markPoly.Layer = layer;
                markPoly.Color = new AciColor(colRenk); // Tip rengi
                markPoly.SetConstantWidth(0.5); // Kalın çizgi
                dxf.Entities.Add(markPoly);

                // 2. KOLON DİKDÖRTGENİ (iç, gerçek boyut)
                var colPoly = new Polyline2D(new List<Polyline2DVertex>
                {
                    new Polyline2DVertex(c0.X, c0.Y),
                    new Polyline2DVertex(c1.X, c1.Y),
                    new Polyline2DVertex(c2.X, c2.Y),
                    new Polyline2DVertex(c3.X, c3.Y)
                }, true);
                colPoly.Layer = layer;
                colPoly.Color = new AciColor(7); // White (kolon kendisi)
                dxf.Entities.Add(colPoly);

                // Etiket
                string label = col.RebarLabel ?? col.TypeLabel ?? col.Name;
                MText txt = new MText(label, new Vector2(cx + mHw + 2, cy), 1.5);
                txt.Layer = layer;
                txt.Color = new AciColor(7); // White
                dxf.Entities.Add(txt);
            }
        }

        // ============================
        // KOLON TİP DETAYLARI (ZWCAD CizimVeIsaretlemeYap + TipCiz birebir)
        // ============================
        private static void DrawColumnTypeDetails(
            DxfDocument dxf,
            List<ColumnRebarData> allColumns,
            List<ColumnRebarData> storyColumns,
            List<IGrouping<string, ColumnRebarData>> tipGruplari,
            Dictionary<string, short> tipRenkMap,
            List<string> storyOrder,
            Layer layer)
        {
            // Detay alanı planın altında
            double detailStartX = 0;
            double detailStartY = -600;

            double curX = detailStartX;
            double rowY = detailStartY;
            double minX = curX, maxX = curX, minY = detailStartY;

            int tipSayaci = 1;

            foreach (var tipGrup in tipGruplari)
            {
                var temsilci = tipGrup.First();
                int adet = tipGrup.Count();
                string tipAdi = temsilci.TypeLabel ?? $"Tip {tipSayaci}";

                // Donatı bilgisini parse et
                int donatiAdet = 0;
                int donatiCap = 0;
                ParseRebarLabel(temsilci.RebarLabel, out donatiAdet, out donatiCap);

                double b = temsilci.Width * 100; // m -> cm
                double h = temsilci.Depth * 100;

                short renk = tipRenkMap.ContainsKey(tipGrup.Key) ? tipRenkMap[tipGrup.Key] : (short)7;

                // ZWCAD TipCiz'deki baslik formatı
                double bCm = Math.Min(b, h);
                double hCm = Math.Max(b, h);
                string baslik = $"{tipAdi} ({bCm:0}x{hCm:0}) (Adet:{adet})";

                double curY = rowY;
                int? oncekiCap = donatiCap > 0 ? (int?)donatiCap : null;

                // Ana tip kutucugu çiz
                TipCiz(dxf, curX, curY, b, h, donatiCap, renk, baslik, layer);

                if (curX < minX) minX = curX;
                if (curX + FixedBoxWidth > maxX) maxX = curX + FixedBoxWidth;
                if (curY - FixedBoxHeight < minY) minY = curY - FixedBoxHeight;

                curY -= (FixedBoxHeight + 50.0);

                // --- DARALMA KONTROLÜ ---
                // Bu tipteki kolonların aynı pozisyonunda (X,Y) farklı katlarda boyut değişimi var mı?
                var tipKolonlari = tipGrup.ToList();
                if (tipKolonlari.Count > 0 && storyOrder != null && storyOrder.Count > 1)
                {
                    var refCol = tipKolonlari[0];

                    // Aynı pozisyondaki tüm katlardaki kolonları bul (kat sırasına göre)
                    var zincir = allColumns
                        .Where(c => Math.Abs(c.X - refCol.X) < 0.01 && Math.Abs(c.Y - refCol.Y) < 0.01)
                        .OrderBy(c => storyOrder.IndexOf(c.Story))
                        .ToList();

                    // Boyut değişimlerini tespit et
                    string oncekiBoyut = $"{bCm:0}x{hCm:0}";
                    foreach (var zCol in zincir)
                    {
                        double zB = zCol.Width * 100;
                        double zH = zCol.Depth * 100;
                        double zBCm = Math.Min(zB, zH);
                        double zHCm = Math.Max(zB, zH);
                        string yeniBoyut = $"{zBCm:0}x{zHCm:0}";

                        if (yeniBoyut != oncekiBoyut)
                        {
                            // Daralma var! Altına çiz
                            string daralmaBaslik = $"{zCol.Story} Daralması";
                            int daralmaCap = oncekiCap ?? donatiCap;

                            TipCiz(dxf, curX, curY, zB, zH, daralmaCap, renk, daralmaBaslik, layer);

                            if (curY - FixedBoxHeight < minY) minY = curY - FixedBoxHeight;
                            curY -= (FixedBoxHeight + 50.0);
                            oncekiBoyut = yeniBoyut;
                        }
                    }
                }

                curX += (FixedBoxWidth + 35.0);
                tipSayaci++;
            }

            // Ana çerçeve (ZWCAD: ColorIndex=7, ConstantWidth=5.0)
            double cerceveY = minY - 100;
            var mainFrame = new Polyline2D(new List<Polyline2DVertex>
            {
                new Polyline2DVertex(minX - 100, cerceveY),
                new Polyline2DVertex(maxX + 100, cerceveY),
                new Polyline2DVertex(maxX + 100, rowY + 100),
                new Polyline2DVertex(minX - 100, rowY + 100)
            }, true);
            mainFrame.Layer = layer;
            mainFrame.Color = new AciColor(7);
            mainFrame.SetConstantWidth(5.0);
            dxf.Entities.Add(mainFrame);

            // Başlık (ZWCAD: TextHeight=125.0, ColorIndex=2)
            MText title = new MText(
                "KOLON DONESİ",
                new Vector2((minX + maxX) / 2 + 100, rowY + 150),
                125.0);
            title.Layer = layer;
            title.Color = new AciColor(2);
            title.AttachmentPoint = MTextAttachmentPoint.BottomCenter;
            dxf.Entities.Add(title);
        }

        // ============================
        // TipCiz (ZWCAD kodundan birebir)
        // ============================
        private static void TipCiz(
            DxfDocument dxf, double refX, double refY,
            double b, double h, int rebarDia, short renk, string baslik,
            Layer layer)
        {
            double boxTopY = refY, boxBottomY = refY - FixedBoxHeight;
            double centerX = refX + FixedBoxWidth / 2.0;

            // ZWCAD birebir koordinatlar
            double t1_PosY = boxTopY - 20.0;
            double infoText_StartY = t1_PosY - 15.0;
            double colTopY = boxTopY - 110.0;
            double insX = centerX - (b / 2.0);
            double insY = colTopY - h;

            int adetX = GetDonatiAdedi(b);
            int adetY = GetDonatiAdedi(h);
            int toplamAdet = (2 * adetX) + (2 * adetY) - 4;

            // Çap: ETABS'ten gelen çap, yoksa default 16
            int kullanilanCap = rebarDia > 0 ? rebarDia : 16;
            double donatiOrani = (toplamAdet * Math.PI * Math.Pow(kullanilanCap / 20.0, 2) / (b * h)) * 100;

            // Kolon dış çizgisi (ZWCAD: ColorIndex=1 Red)
            var kol = new Polyline2D(new List<Polyline2DVertex>
            {
                new Polyline2DVertex(insX, insY),
                new Polyline2DVertex(insX + b, insY),
                new Polyline2DVertex(insX + b, insY + h),
                new Polyline2DVertex(insX, insY + h)
            }, true);
            kol.Layer = layer;
            kol.Color = new AciColor(1); // Red
            dxf.Entities.Add(kol);

            // Ölçüler (ZWCAD: RotatedDimension, Layer=DONE-AS)
            // Alt ölçü (B)
            Line dimLineB1 = new Line(new Vector2(insX, insY), new Vector2(insX, insY - 20));
            dimLineB1.Layer = layer; dimLineB1.Color = new AciColor(3);
            dxf.Entities.Add(dimLineB1);
            Line dimLineB2 = new Line(new Vector2(insX + b, insY), new Vector2(insX + b, insY - 20));
            dimLineB2.Layer = layer; dimLineB2.Color = new AciColor(3);
            dxf.Entities.Add(dimLineB2);
            Line dimLineB3 = new Line(new Vector2(insX, insY - 15), new Vector2(insX + b, insY - 15));
            dimLineB3.Layer = layer; dimLineB3.Color = new AciColor(3);
            dxf.Entities.Add(dimLineB3);
            MText dimBText = new MText($"{b:0}", new Vector2(insX + b / 2, insY - 18), 4.0);
            dimBText.AttachmentPoint = MTextAttachmentPoint.TopCenter;
            dimBText.Layer = layer; dimBText.Color = new AciColor(3);
            dxf.Entities.Add(dimBText);

            // Sol ölçü (H)
            Line dimLineH1 = new Line(new Vector2(insX, insY), new Vector2(insX - 20, insY));
            dimLineH1.Layer = layer; dimLineH1.Color = new AciColor(3);
            dxf.Entities.Add(dimLineH1);
            Line dimLineH2 = new Line(new Vector2(insX, insY + h), new Vector2(insX - 20, insY + h));
            dimLineH2.Layer = layer; dimLineH2.Color = new AciColor(3);
            dxf.Entities.Add(dimLineH2);
            Line dimLineH3 = new Line(new Vector2(insX - 15, insY), new Vector2(insX - 15, insY + h));
            dimLineH3.Layer = layer; dimLineH3.Color = new AciColor(3);
            dxf.Entities.Add(dimLineH3);
            MText dimHText = new MText($"{h:0}", new Vector2(insX - 18, insY + h / 2), 4.0);
            dimHText.AttachmentPoint = MTextAttachmentPoint.MiddleRight;
            dimHText.Layer = layer; dimHText.Color = new AciColor(3);
            dxf.Entities.Add(dimHText);

            // İç etriye çizgisi (ZWCAD: ColorIndex=7 White)
            var ic = new Polyline2D(new List<Polyline2DVertex>
            {
                new Polyline2DVertex(insX + Paspayi, insY + Paspayi),
                new Polyline2DVertex(insX + b - Paspayi, insY + Paspayi),
                new Polyline2DVertex(insX + b - Paspayi, insY + h - Paspayi),
                new Polyline2DVertex(insX + Paspayi, insY + h - Paspayi)
            }, true);
            ic.Layer = layer;
            ic.Color = new AciColor(7); // White
            dxf.Entities.Add(ic);

            // Donatı yerleşimi (ZWCAD birebir)
            double off = Paspayi + SabitGorselYaricap;
            double sx = (adetX > 1) ? (b - 2 * off) / (adetX - 1) : 0;
            double sy = (adetY > 1) ? (h - 2 * off) / (adetY - 1) : 0;

            List<Vector2> topBars = new List<Vector2>();
            List<Vector2> bottomBars = new List<Vector2>();
            List<Vector2> leftBars = new List<Vector2>();
            List<Vector2> rightBars = new List<Vector2>();

            for (int i = 0; i < adetX; i++)
            {
                Vector2 pTop = new Vector2(insX + off + i * sx, insY + h - off);
                Vector2 pBot = new Vector2(insX + off + i * sx, insY + off);
                topBars.Add(pTop); bottomBars.Add(pBot);
                DonatiEkle(dxf, pTop, layer); // ColorIndex=7
                DonatiEkle(dxf, pBot, layer);
            }

            for (int j = 0; j < adetY; j++)
            {
                Vector2 pLeft = new Vector2(insX + off, insY + off + j * sy);
                Vector2 pRight = new Vector2(insX + b - off, insY + off + j * sy);
                leftBars.Add(pLeft); rightBars.Add(pRight);
                if (j > 0 && j < adetY - 1)
                {
                    DonatiEkle(dxf, pLeft, layer);
                    DonatiEkle(dxf, pRight, layer);
                }
            }

            // Etriye / Çıroz konfigürasyonu (ZWCAD birebir)
            KonfigureEt(dxf, topBars, bottomBars, layer);
            KonfigureEt(dxf, rightBars, leftBars, layer);

            // Tip kutusu (ZWCAD: ColorIndex=renk)
            var box = new Polyline2D(new List<Polyline2DVertex>
            {
                new Polyline2DVertex(refX, boxBottomY),
                new Polyline2DVertex(refX + FixedBoxWidth, boxBottomY),
                new Polyline2DVertex(refX + FixedBoxWidth, boxTopY),
                new Polyline2DVertex(refX, boxTopY)
            }, true);
            box.Layer = layer;
            box.Color = new AciColor(renk);
            dxf.Entities.Add(box);

            // Metinler (ZWCAD birebir renk ve boyut)
            // t1: baslik, TextHeight=12.0, ColorIndex=3 (Green), BottomCenter
            MText t1 = new MText(baslik, new Vector2(centerX, t1_PosY), 12.0);
            t1.AttachmentPoint = MTextAttachmentPoint.BottomCenter;
            t1.Layer = layer;
            t1.Color = new AciColor(3); // Green
            dxf.Entities.Add(t1);

            // t2: donatı bilgisi, TextHeight=10.0, ColorIndex=2 (Yellow), TopCenter
            // %%c = çap sembolü (Ø) in CAD
            MText t2 = new MText($"{toplamAdet}%%c{kullanilanCap}",
                new Vector2(centerX, infoText_StartY), 10.0);
            t2.AttachmentPoint = MTextAttachmentPoint.TopCenter;
            t2.Layer = layer;
            t2.Color = new AciColor(2); // Yellow
            dxf.Entities.Add(t2);

            // t3: donatı oranı, TextHeight=10.0, ColorIndex=2, TopCenter
            MText t3 = new MText($"%{donatiOrani:F2}",
                new Vector2(centerX, infoText_StartY - 20.0), 10.0);
            t3.AttachmentPoint = MTextAttachmentPoint.TopCenter;
            t3.Layer = layer;
            t3.Color = new AciColor(2); // Yellow
            dxf.Entities.Add(t3);

            // tEtriye: TextHeight=6.0, ColorIndex=2, TopCenter
            // %%C = çap sembolü
            MText tEtriye = new MText("Etr.- Çrz. %%C12/15/10",
                new Vector2(centerX, infoText_StartY - 40.0), 6.0);
            tEtriye.AttachmentPoint = MTextAttachmentPoint.TopCenter;
            tEtriye.Layer = layer;
            tEtriye.Color = new AciColor(2); // Yellow
            dxf.Entities.Add(tEtriye);
        }

        // --- DONATI EKLEME (ZWCAD: Circle, ColorIndex=7, Radius=1.75) ---
        private static void DonatiEkle(DxfDocument dxf, Vector2 center, Layer layer)
        {
            Circle c = new Circle(new Vector3(center.X, center.Y, 0), SabitGorselYaricap);
            c.Layer = layer;
            c.Color = new AciColor(7); // White
            dxf.Entities.Add(c);
        }

        // --- ÇIROZ ÇİZİMİ (ZWCAD kodundan birebir, bulge arc'lar dahil) ---
        private static void CirozCiz(DxfDocument dxf, Vector2 c1, Vector2 c2, Layer layer)
        {
            double r = 2.75;

            // Yön vektörü
            double vx = c2.X - c1.X;
            double vy = c2.Y - c1.Y;
            double vLen = Math.Sqrt(vx * vx + vy * vy);
            if (vLen < 0.001) return;
            vx /= vLen; vy /= vLen; // normalize

            // Dik vektör p = (-vy, vx)
            double px = -vy;
            double py = vx;

            // Bulge değerleri (ZWCAD birebir)
            double bulge90 = -Math.Tan((90.0 / 4.0) * (Math.PI / 180.0));
            double bulge151 = -Math.Tan((151.0 / 4.0) * (Math.PI / 180.0));

            // Üst taraf noktaları
            double pTopMainX = c1.X + px * r;
            double pTopMainY = c1.Y + py * r;

            double pTopArcStartX = c1.X - vx * r;
            double pTopArcStartY = c1.Y - vy * r;

            double pTopTipStartX = pTopArcStartX - px * 5.0;
            double pTopTipStartY = pTopArcStartY - py * 5.0;

            // Alt taraf noktaları
            double pBotMainX = c2.X + px * r;
            double pBotMainY = c2.Y + py * r;

            // rvEnd = p.RotateBy(-151°)
            double ang151 = -151.0 * Math.PI / 180.0;
            double rvEndX = px * Math.Cos(ang151) - py * Math.Sin(ang151);
            double rvEndY = px * Math.Sin(ang151) + py * Math.Cos(ang151);

            double pBotArcEndX = c2.X + rvEndX * r;
            double pBotArcEndY = c2.Y + rvEndY * r;

            // tanBot = rvEnd.RotateBy(-90°)
            double ang90 = -90.0 * Math.PI / 180.0;
            double tanBotX = rvEndX * Math.Cos(ang90) - rvEndY * Math.Sin(ang90);
            double tanBotY = rvEndX * Math.Sin(ang90) + rvEndY * Math.Cos(ang90);
            double tanLen = Math.Sqrt(tanBotX * tanBotX + tanBotY * tanBotY);
            if (tanLen > 0.001) { tanBotX /= tanLen; tanBotY /= tanLen; }

            double pBotTipEndX = pBotArcEndX + tanBotX * 7.5;
            double pBotTipEndY = pBotArcEndY + tanBotY * 7.5;

            // ZWCAD Polyline birebir: 6 vertex, bulge at vertex 1 and 3
            var vertices = new List<Polyline2DVertex>
            {
                new Polyline2DVertex(pTopTipStartX, pTopTipStartY),                   // 0: straight
                new Polyline2DVertex(pTopArcStartX, pTopArcStartY) { Bulge = bulge90 }, // 1: arc (bulge90)
                new Polyline2DVertex(pTopMainX, pTopMainY),                             // 2: straight
                new Polyline2DVertex(pBotMainX, pBotMainY) { Bulge = bulge151 },        // 3: arc (bulge151)
                new Polyline2DVertex(pBotArcEndX, pBotArcEndY),                         // 4: straight
                new Polyline2DVertex(pBotTipEndX, pBotTipEndY)                          // 5: end
            };

            var pl = new Polyline2D(vertices, false);
            pl.Layer = layer;
            pl.Color = new AciColor(4); // Cyan (ZWCAD: ColorIndex=4)
            dxf.Entities.Add(pl);
        }

        // --- İÇ ETRİYE ÇİZİMİ (ZWCAD: Closed Polyline, ColorIndex=7) ---
        private static void IcEtriyeCiz(DxfDocument dxf, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Layer layer)
        {
            double off = SabitGorselYaricap;
            double pMinX = Math.Min(Math.Min(p1.X, p2.X), Math.Min(p3.X, p4.X)) - off;
            double pMaxX = Math.Max(Math.Max(p1.X, p2.X), Math.Max(p3.X, p4.X)) + off;
            double pMinY = Math.Min(Math.Min(p1.Y, p2.Y), Math.Min(p3.Y, p4.Y)) - off;
            double pMaxY = Math.Max(Math.Max(p1.Y, p2.Y), Math.Max(p3.Y, p4.Y)) + off;

            var pl = new Polyline2D(new List<Polyline2DVertex>
            {
                new Polyline2DVertex(pMinX, pMinY),
                new Polyline2DVertex(pMaxX, pMinY),
                new Polyline2DVertex(pMaxX, pMaxY),
                new Polyline2DVertex(pMinX, pMaxY)
            }, true);
            pl.Layer = layer;
            pl.Color = new AciColor(7); // White (ZWCAD: ColorIndex=7)
            dxf.Entities.Add(pl);
        }

        // --- KONFİGÜRASYON (ZWCAD kodundan birebir) ---
        private static void KonfigureEt(DxfDocument dxf, List<Vector2> side1, List<Vector2> side2, Layer layer)
        {
            int count = side1.Count;

            void GuvenliCiroz(int idx)
            {
                if (idx > 0 && idx < count - 1)
                {
                    if (idx % 4 == 0)
                        CirozCiz(dxf, side1[idx], side2[idx], layer);
                    else
                        CirozCiz(dxf, side2[idx], side1[idx], layer);
                }
            }

            switch (count)
            {
                case 3: break;
                case 4: GuvenliCiroz(1); break;
                case 5: GuvenliCiroz(2); break;
                case 6:
                    IcEtriyeCiz(dxf, side1[2], side1[3], side2[3], side2[2], layer);
                    break;
                case 7:
                    IcEtriyeCiz(dxf, side1[2], side1[4], side2[4], side2[2], layer);
                    break;
                case 9:
                    IcEtriyeCiz(dxf, side1[2], side1[6], side2[6], side2[2], layer);
                    GuvenliCiroz(4);
                    break;
                case 11:
                    IcEtriyeCiz(dxf, side1[2], side1[8], side2[8], side2[2], layer);
                    GuvenliCiroz(4);
                    GuvenliCiroz(6);
                    break;
                case 13:
                    IcEtriyeCiz(dxf, side1[4], side1[8], side2[8], side2[4], layer);
                    GuvenliCiroz(2);
                    GuvenliCiroz(6);
                    GuvenliCiroz(10);
                    break;
                case 15:
                    IcEtriyeCiz(dxf, side1[0], side1[10], side2[10], side2[0], layer);
                    IcEtriyeCiz(dxf, side1[4], side1[14], side2[14], side2[4], layer);
                    GuvenliCiroz(2);
                    GuvenliCiroz(6);
                    GuvenliCiroz(8);
                    GuvenliCiroz(12);
                    break;
                case 17:
                    IcEtriyeCiz(dxf, side1[0], side1[12], side2[12], side2[0], layer);
                    IcEtriyeCiz(dxf, side1[4], side1[16], side2[16], side2[4], layer);
                    GuvenliCiroz(2);
                    GuvenliCiroz(6);
                    GuvenliCiroz(8);
                    GuvenliCiroz(10);
                    GuvenliCiroz(14);
                    break;
                case 19:
                    IcEtriyeCiz(dxf, side1[0], side1[12], side2[12], side2[0], layer);
                    IcEtriyeCiz(dxf, side1[6], side1[18], side2[18], side2[6], layer);
                    GuvenliCiroz(2);
                    GuvenliCiroz(4);
                    GuvenliCiroz(8);
                    GuvenliCiroz(10);
                    GuvenliCiroz(14);
                    GuvenliCiroz(16);
                    break;
                case 21:
                    IcEtriyeCiz(dxf, side1[0], side1[14], side2[14], side2[0], layer);
                    IcEtriyeCiz(dxf, side1[6], side1[20], side2[20], side2[6], layer);
                    GuvenliCiroz(2);
                    GuvenliCiroz(4);
                    GuvenliCiroz(8);
                    GuvenliCiroz(10);
                    GuvenliCiroz(12);
                    GuvenliCiroz(16);
                    GuvenliCiroz(18);
                    break;
                default:
                    for (int i = 2; i < count - 1; i += 2)
                        GuvenliCiroz(i);
                    break;
            }
        }

        // --- DONATI ADEDİ HESABI (ZWCAD kodundan birebir) ---
        private static int GetDonatiAdedi(double k)
        {
            if (k <= 35) return 3;
            if (k <= 50) return 4;
            if (k <= 60) return 5;
            if (k <= 70) return 6;
            if (k <= 80) return 7;
            if (k <= 90) return 8;
            if (k <= 110) return 9;
            if (k <= 130) return 11;
            if (k <= 160) return 13;
            if (k <= 180) return 15;
            if (k <= 190) return 17;
            if (k <= 240) return 19;
            if (k <= 270) return 21;
            return 23;
        }

        // --- YARDIMCI ---
        private static void ParseRebarLabel(string label, out int count, out int diameter)
        {
            count = 0;
            diameter = 0;
            if (string.IsNullOrEmpty(label)) return;

            string lbl = label.Replace(" ", "");
            string[] parts = lbl.Split('φ');
            if (parts.Length == 2)
            {
                int.TryParse(parts[0], out count);
                int.TryParse(parts[1], out diameter);
            }
        }

        private static Vector2 Rotate(double cx, double cy, double dx, double dy, double cosA, double sinA)
        {
            return new Vector2(
                cx + dx * cosA - dy * sinA,
                cy + dx * sinA + dy * cosA);
        }
    }
}
