using System;
using System.ComponentModel;
using Bogus.DataSets;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

//using System.Reflection.Metadata;
using Library1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZXing.QrCode;
using ZXing.Rendering;
using ZXing;

namespace Library1.Helpers
{
    public class InvoiceReadingService
    {
        public InvoiceReadingService() 
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }
        public byte[] GenerateInvoicePdf(Invoice invoice)
        {
            var document =Document.Create(Container =>
            {
                
                Container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(0, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.ContentFromRightToLeft();


                    page.Header()
                    .Background(Colors.Blue.Medium)
                    .Padding(20)
                    .SkipOnce()
                    //.ContentFromRightToLeft()
                    .Row(row=>
                    {
                        row.RelativeItem()
                        .Column(column =>
                        {
                            column.Item()
                            .Text("Company Name")
                            .FontFamily("Arial")
                            .FontSize(20)
                            .Bold();
                            column.Item()
                            .Text("Company Address")
                            .FontFamily("Arial");
                        });
                        row.RelativeItem()
                        .ShowOnce()
                        .Text("Invoice")
                        .AlignRight()
                        .FontFamily("Arial")
                        .ExtraBlack()
                        .FontSize(30);
                    });


                    page.Content()
                    .PaddingTop(50)
                    .PaddingHorizontal(40)
                    .Column(column=>
                    {
                        // الصفحة الاولى
                        column.Item().Row(row =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column2 =>
                                {
                                    column2.Item()
                                    .AlignCenter()
                                    .Padding(0)
                                    .Height(100)
                                    .Width(300)
                                    .Image("Images\\yanLogo2.png"); //D:\library-angular\Backend-Dotnet\Images\yanLogo2.png
                                    column2.Item()
                                     .AlignCenter()
                                    .Width(350)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Blue.Medium);
                                });
                            });

                            column.Item().PaddingVertical(20)
                            .AlignCenter()
                            .Text("اسم المشروع الكامل - المنطقة - المحافظة")
                            .FontFamily("Times New Roman")
                            .FontSize(25)
                            .Bold()
                            .FontColor(Colors.Blue.Lighten1);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                .AlignLeft()
                                .PaddingVertical(10)
                                .AlignCenter()
                                .Text("رقم الدراسة :")
                                .FontFamily("Times New Roman")
                                .FontSize(16)
                                .FontColor(Colors.Blue.Lighten1);
                                row.RelativeItem()
                                .AlignRight()
                                .PaddingRight(5)
                                .PaddingVertical(10)
                                .AlignCenter()
                                .Text("82365")
                                .FontFamily("Tohama")
                                .FontSize(16)
                                .FontColor(Colors.Red.Lighten1);
                            });
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    column.Item()
                                    .AlignCenter()
                                    .Text("موقع المشروع")
                                    .FontFamily("Times New Roman")
                                    .FontSize(26)
                                    .FontColor(Colors.Blue.Lighten1);
                                    column.Item()
                                        .PaddingTop(10)
                                    .AlignCenter()
                                    .Text("اليمن - عدن")
                                    .FontFamily("Times New Roman")
                                    .FontSize(16)
                                    .FontColor(Colors.Red.Lighten1);
                                });

                                row.RelativeItem().Column(column =>
                                {
                                    column.Item()
                                    .AlignCenter()
                                    .Text("تكلفة المشروع")
                                    .FontFamily("Times New Roman")
                                    .FontSize(26)
                                    .FontColor(Colors.Blue.Lighten1);
                                    column.Item()
                                        .PaddingTop(10)
                                    .AlignCenter()
                                    .Text("5950 ر.س")
                                    .FontFamily("Times New Roman")
                                    .FontSize(16)
                                    .FontColor(Colors.Red.Lighten1);
                                });
                            });
                            column.Item()
                            .AlignCenter()
                            .Padding(20)
                            .Image("Images\\prjPhoto.jpeg");
                            column.Item()
                            .AlignCenter()
                            .PaddingTop(20)
                            .Text(DateTime.Now.ToString("dd/MMM/yyyy"))
                            .FontFamily("Times New Roman")
                            .FontSize(16)
                            .FontColor(Colors.Blue.Lighten1);
                        });

                        // الصفحات المتبقية
                        column.Item().StopPaging().PaddingTop(200).Row(row =>
                        {
                            row.RelativeItem().Column(colmun =>
                            {
                                column.Item().Row(row =>
                                {
                                    row.ConstantItem(40)
                                    .PaddingLeft(10)
                                    .PaddingTop(33)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                    row.ConstantItem(200)
                                    .AlignRight()
                                    .Width(200)
                                    .Height(70)
                                    .Image("Images\\yanLogo2.png");
                                    row.RelativeItem()
                                    .PaddingRight(10)
                                    .PaddingTop(35)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                               });
                                column.Item().Row(row =>
                                {
                                    
                                    row.ConstantItem(40)
                                    .PaddingLeft(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                    row.ConstantItem(130)
                                    .AlignCenter()
                                    .Text("مقدمة عامة")
                                    .FontFamily("Times New Roman")
                                    .FontSize(28)
                                    .FontColor(Colors.Blue.Lighten1)
                                    .Bold();
                                    row.RelativeItem()
                                    .PaddingRight(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                });
                                column.Item().Row(row =>
                                {

                                    row.ConstantItem(40)
                                    .PaddingLeft(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                    row.ConstantItem(180)
                                    .AlignCenter()
                                    .Text("ملخص المشروع")
                                    .FontFamily("Times New Roman")
                                    .FontSize(28)
                                    .FontColor(Colors.Blue.Lighten1)
                                    .Bold();
                                    row.RelativeItem()
                                    .PaddingRight(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                });
                                column.Item().Row(row =>
                                {

                                    row.ConstantItem(40)
                                    .PaddingLeft(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                    row.ConstantItem(380)
                                    .AlignCenter()
                                    .Text("بيان المشكلة وأهمية المشروع ومبرراته")
                                    .FontFamily("Times New Roman")
                                    .FontSize(28)
                                    .FontColor(Colors.Blue.Lighten1)
                                    .Bold();
                                    row.RelativeItem()
                                    .PaddingRight(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                });
                                column.Item().Row(row =>
                                {

                                    row.ConstantItem(40)
                                    .PaddingLeft(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                    row.ConstantItem(140)
                                    .AlignCenter()
                                    .Text("إطار المشروع")
                                    .FontFamily("Times New Roman")
                                    .FontSize(28)
                                    .FontColor(Colors.Blue.Lighten1)
                                    .Bold();
                                    row.RelativeItem()
                                    .PaddingRight(10)
                                    .PaddingTop(20)
                                    .LineHorizontal(2)
                                    .LineColor(Colors.Green.Lighten1);
                                });

                            });
                        });

                        // الصفحة الاخيرة



                        #region
                        //column.Item().PaddingTop(50).Table(table =>
                        //{
                        //    table.ColumnsDefinition(columns =>
                        //    {
                        //        columns.ConstantColumn(40); //NO.
                        //        columns.RelativeColumn(); //Description.
                        //        columns.ConstantColumn(50); //Quantity.
                        //        columns.ConstantColumn(60); //Unit Price.
                        //        columns.ConstantColumn(70); //Total.
                        //    });
                        //    table.Header(header =>
                        //    {
                        //        header.Cell().Padding(4).Text("#").Bold();
                        //        header.Cell().Padding(4).Text("Description").Bold();
                        //        header.Cell().Padding(4).Text("Qty").AlignRight().Bold();
                        //        header.Cell().Padding(4).Text("Price").AlignRight().Bold();
                        //        header.Cell().Padding(4).Text("Total").AlignRight().Bold();

                        //        header.Cell().ColumnSpan(5).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                        //    });
                        //    for(var i=0;i<invoice.InvoiceItem.Count;i++)
                        //    {
                        //        var backgroundColor = i % 2 == 0 ?
                        //        Color.FromHex("#ffffff") :
                        //        Color.FromHex("#f0f0f0");
                        //        var invoiceItem = invoice.InvoiceItem[i];
                        //        table.Cell().Background(backgroundColor).Padding(4).Text((i + 1).ToString());
                        //        table.Cell().Background(backgroundColor).Padding(4).Text(invoiceItem.Description);
                        //        table.Cell().Background(backgroundColor).Padding(4).Text(invoiceItem.Quantity);
                        //        table.Cell().Background(backgroundColor).Padding(4).Text(invoiceItem.UnitPrice);
                        //        table.Cell().Background(backgroundColor).Padding(4).Text(invoiceItem.Total);
                        //    }
                        //});
                        #endregion
                    });
                    

                    page.Footer()
                    .Background(Colors.Red.Medium)
                    .Padding(20)
                    .SkipOnce()
                    .Row(row =>
                    {
                        
                        row.RelativeItem()
                        .Column(column =>
                        {
                            column.Item()
                            .Text("Yanabiaa")
                            .FontFamily("Arial")
                            .FontSize(20)
                            .Bold();
                            column.Item()
                            .Text("https://Yanabia.org")
                            .FontFamily("Arial")
                            ;
                            column.Item().AlignCenter().Text(text =>
                            {
                                text.CurrentPageNumber();
                                text.Span(" / ");
                                text.TotalPages();
                            });
                        });
                        row.RelativeItem()
                        .ShowOnce()
                        .Text("YKF")
                        .AlignRight()
                        .FontFamily("Arial")
                        .ExtraBlack()
                        .FontSize(30);

                        const string url = "https://yanabia.org";
                        row.ConstantItem(4, Unit.Centimetre)
                         .AspectRatio(1)
                         .Background(Colors.Red.Medium)
                        .Svg(size =>
                        {
                            var writer = new QRCodeWriter();
                            var qrCode = writer.encode(url, BarcodeFormat.QR_CODE, (int)size.Width, (int)size.Height);
                            var renderer = new SvgRenderer { FontName = "Lato" };
                            return renderer.Render(qrCode, BarcodeFormat.EAN_13, null).Content;
                         });
                    });
                    
                });


                //Container.Page(page =>
                //{

                //});
            });

            document.ShowInCompanion();
            return document.GeneratePdf();
        }
    }
}
