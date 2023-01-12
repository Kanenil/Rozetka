using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed()
        {
            using (EFAppContext dataContext = new EFAppContext())
            {
                SeedCategories(dataContext);
                SeedProducts(dataContext);
                SeedImages(dataContext);
            }
        }
        private static void SeedCategories(EFAppContext dataContext)
        {
            if (!dataContext.Categories.Any())
            {
                var noutbuki = new CategoryEntity
                {
                    Name = "Ноутбуки",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/noutbuki.png"
                };

                var kompyutery = new CategoryEntity
                {
                    Name = "Комп'ютери",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/kompyutery.png"
                };

                var monitory = new CategoryEntity
                {
                    Name = "Монітори",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/monitory.png"
                };
                
                var mobilnye_telefony = new CategoryEntity
                {
                    Name = "Мобільні телефони",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/smartfony_tv_i_elektronika/mobilnye_telefony.jpg"
                };
                
                var televizory = new CategoryEntity
                {
                    Name = "Телевізори",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/smartfony_tv_i_elektronika/televizory.jpg"
                };

                dataContext.Categories.Add(noutbuki);
                dataContext.Categories.Add(kompyutery);
                dataContext.Categories.Add(monitory);
                dataContext.Categories.Add(mobilnye_telefony);
                dataContext.Categories.Add(televizory);
                dataContext.SaveChanges();
            }
        }
        private static void SeedProducts(EFAppContext dataContext)
        {
            if (!dataContext.Products.Any())
            {
                ICategoryRepository categoryRepository = new CategoryRepository(dataContext);
                var categories = categoryRepository.GetAll();

                #region Ноутбуки
                var notebook = categories.Where(c => c.Name == "Ноутбуки").First().Id;

                var acerAspire = new ProductEntity
                {
                    Name = "Acer Aspire 7 A715-42G-R8BL (NH.QDLEU.008) Charcoal Black",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / AMD Ryzen 5 5500U (2.1 - 4.0 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050 Ti, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.15 кг / чорний",
                    Price = 36999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var macBookAir = new ProductEntity
                {
                    Name = "Apple MacBook Air 13\" M1 256GB 2020 (MGN93) Silver",
                    Description = "Екран 13.3\" Retina (2560x1600) WQXGA, глянсовий / Apple M1 / RAM 8 ГБ / SSD 256 ГБ / Apple M1 Graphics / Wi-Fi / Bluetooth / macOS Big Sur / 1.29 кг / сріблястий",
                    Price = 42999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var lenovoIdeaPad = new ProductEntity
                {
                    Name = "Lenovo IdeaPad Gaming 3 15IHU6 (82K101FJRA) Shadow Black",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i5-11320H (2.5 - 4.5 ГГц) / RAM 8 ГБ / SSD 256 ГБ / nVidia GeForce GTX 1650, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.25 кг / чорний",
                    Price = 29999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(acerAspire);
                dataContext.Products.Add(macBookAir);
                dataContext.Products.Add(lenovoIdeaPad);
                #endregion
                #region Комп'ютери
                var computers = categories.Where(c => c.Name == "Комп'ютери").First().Id;

                var artlineGaming = new ProductEntity
                {
                    Name = "Artline Gaming X51 v07 (X51v07)",
                    Description = "Intel Core i5-10400F (2.9 — 4.3 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 240 ГБ / nVidia GeForce GTX 1660, 6 ГБ / без ОД / LAN / без ОС",
                    Price = 28999,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                var QUBE = new ProductEntity
                {
                    Name = "QUBE Ігровий QB R i5 10400F RTX 3060 12 GB 1621",
                    Description = "Intel Core i5-10400F (2.9 — 4.3 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 240 ГБ / nVidia GeForce RTX 3060, 12 ГБ / без ОД / LAN / без ОС",
                    Price = 35799,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(artlineGaming);
                dataContext.Products.Add(QUBE);
                #endregion
                #region Монітори
                var monitor = categories.Where(c => c.Name == "Монітори").First().Id;

                var samsungOdyssey = new ProductEntity
                {
                    Name = "Samsung Odyssey G7 S28AG702 (LS28AG702NIXCI) 4K HDR400 / IPS 8-Bit / 144Гц / sRGB 99% / G-SYNC Compatible",
                    Description = "Діагональ дисплея\r\n28\"\r\nМаксимальна роздільна здатність дисплея\r\n3840x2160 (4K UltraHD)\r\nЧас реакції матриці\r\n1 мс\r\nЯскравість дисплея\r\n400 кд/м2\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1000:1 (Typ)\r\nОсобливості\r\nFlicker-Free\r\nUSB-концентратор\r\nБезрамковий (Сinema screen)\r\nПоворотний екран (Pivot)\r\nРегулювання за висотою",
                    Price = 21999,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                var asusTUF = new ProductEntity
                {
                    Name = "Asus TUF Gaming VG259QR (90LM0530-B03370) 8-Bit / 165Hz / Adaptive-Sync / G-Sync Compatible",
                    Description = "Діагональ дисплея\r\n24.5\"\r\nМаксимальна роздільна здатність дисплея\r\n1920x1080 (FullHD)\r\nЧас реакції матриці\r\n1 мс MPRT\r\nЯскравість дисплея\r\n300 кд/м²\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1000:1\r\nОсобливості\r\nFlicker-Free\r\nБезрамковий (Сinema screen)\r\nПоворотний екран (Pivot)\r\nРегулювання за висотою",
                    Price = 9399,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(samsungOdyssey);
                dataContext.Products.Add(asusTUF);
                #endregion
                #region Мобільні телефони
                var phone = categories.Where(c => c.Name == "Мобільні телефони").First().Id;

                var appleiPhone13 = new ProductEntity
                {
                    Name = "Apple iPhone 13 128GB Pink (MLPH3HU/A)",
                    Description = "Екран (6.1\", OLED (Super Retina XDR), 2532x1170) / Apple A15 Bionic / подвійна основна камера: 12 Мп + 12 Мп, фронтальна камера: 12 Мп / 128 ГБ вбудованої пам'яті / 3G / LTE / 5G / GPS / Nano-SIM, eSIM / iOS 15",
                    Price = 35999,
                    CategoryId = phone,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(appleiPhone13);
                #endregion
                #region Телевізори
                var tv = categories.Where(c => c.Name == "Телевізори").First().Id;

                var samsung = new ProductEntity
                {
                    Name = "Samsung UE50AU7100UXUA",
                    Description = "Діагональ екрана\r\n50\"\r\nРоздільна здатність\r\n3840x2160\r\nПлатформа\r\nTizen\r\nДіапазони цифрового тюнера\r\nDVB-C\r\nDVB-S2\r\nDVB-T2",
                    Price = 19799,
                    CategoryId = tv,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(samsung);
                #endregion

                dataContext.SaveChanges();
            }
        }
        private static void SeedImages(EFAppContext dataContext)
        {
            if (!dataContext.ProductImages.Any())
            {
                IProductRepository productRepository = new ProductRepository(dataContext);
                var products = productRepository.GetAll();

                #region Ноутбуки
                #region Acer Aspire 7 A715-42G-R8BL (NH.QDLEU.008) Charcoal Black
                var AcerAspire = products.Where(x => x.Name == "Acer Aspire 7 A715-42G-R8BL (NH.QDLEU.008) Charcoal Black").First().Id;

                var AcerAspirephoto1 = new ProductImageEntity
                {
                   Name = @"https://content1.rozetka.com.ua/goods/images/big/254116608.jpg",
                   Priority = 1,
                   ProductId = AcerAspire,
                   DateCreated = DateTime.Now
                };

                var AcerAspirephoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/290848838.jpg",
                    Priority = 2,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                var AcerAspirephoto3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/254116609.jpg",
                    Priority = 3,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                var AcerAspirephoto4 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/254116610.jpg",
                    Priority = 4,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                var AcerAspirephoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/254116611.jpg",
                    Priority = 5,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AcerAspirephoto1);
                dataContext.ProductImages.Add(AcerAspirephoto2);
                dataContext.ProductImages.Add(AcerAspirephoto3);
                dataContext.ProductImages.Add(AcerAspirephoto4);
                dataContext.ProductImages.Add(AcerAspirephoto5);
                #endregion
                #region Apple MacBook Air 13\" M1 256GB 2020 (MGN93) Silver
                var MacBookAir = products.Where(x => x.Name == "Apple MacBook Air 13\" M1 256GB 2020 (MGN93) Silver").First().Id;

                var MacBookAirphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/215770075.jpg",
                    Priority = 1,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/215770142.jpg",
                    Priority = 2,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/215770223.jpg",
                    Priority = 3,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/215770290.jpg",
                    Priority = 4,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/215770341.jpg",
                    Priority = 5,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(MacBookAirphoto1);
                dataContext.ProductImages.Add(MacBookAirphoto2);
                dataContext.ProductImages.Add(MacBookAirphoto3);
                dataContext.ProductImages.Add(MacBookAirphoto4);
                dataContext.ProductImages.Add(MacBookAirphoto5);
                #endregion
                #region Lenovo IdeaPad Gaming 3 15IHU6 (82K101FJRA) Shadow Black
                var LenovoIdeaPad = products.Where(x => x.Name == "Lenovo IdeaPad Gaming 3 15IHU6 (82K101FJRA) Shadow Black").First().Id;

                var LenovoIdeaPadphoto1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/280598520.jpg",
                    Priority = 1,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/284974732.jpg",
                    Priority = 2,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/280598574.jpg",
                    Priority = 3,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/280598621.jpg",
                    Priority = 4,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/280598671.jpg",
                    Priority = 5,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(LenovoIdeaPadphoto1);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto2);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto3);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto4);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto5);
                #endregion
                #endregion
                #region Комп'ютери
                #region Artline Gaming X51 v07 (X51v07)
                var ArtlineGaming = products.Where(x => x.Name == "Artline Gaming X51 v07 (X51v07)").First().Id;

                var ArtlineGamingphoto1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/244859893.jpg",
                    Priority = 1,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/244859895.jpg",
                    Priority = 2,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/244859897.jpg",
                    Priority = 3,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/244859902.jpg",
                    Priority = 4,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/244859905.jpg",
                    Priority = 5,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(ArtlineGamingphoto1);
                dataContext.ProductImages.Add(ArtlineGamingphoto2);
                dataContext.ProductImages.Add(ArtlineGamingphoto3);
                dataContext.ProductImages.Add(ArtlineGamingphoto4);
                dataContext.ProductImages.Add(ArtlineGamingphoto5);
                #endregion
                #region QUBE Ігровий QB R i5 10400F RTX 3060 12 GB 1621
                var QUBE = products.Where(x => x.Name == "QUBE Ігровий QB R i5 10400F RTX 3060 12 GB 1621").First().Id;

                var QUBEphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/255402319.jpg",
                    Priority = 1,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/255402320.jpg",
                    Priority = 2,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/255402321.jpg",
                    Priority = 3,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/255402322.jpg",
                    Priority = 4,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/255402328.jpg",
                    Priority = 5,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(QUBEphoto1);
                dataContext.ProductImages.Add(QUBEphoto2);
                dataContext.ProductImages.Add(QUBEphoto3);
                dataContext.ProductImages.Add(QUBEphoto4);
                dataContext.ProductImages.Add(QUBEphoto5);
                #endregion
                #endregion
                #region Монітори
                #region Samsung Odyssey G7 S28AG702 (LS28AG702NIXCI) 4K HDR400 / IPS 8-Bit / 144Гц / sRGB 99% / G-SYNC Compatible
                var SamsungOdyssey = products.Where(x => x.Name == "Samsung Odyssey G7 S28AG702 (LS28AG702NIXCI) 4K HDR400 / IPS 8-Bit / 144Гц / sRGB 99% / G-SYNC Compatible").First().Id;

                var SamsungOdysseyphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/250044076.jpg",
                    Priority = 1,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/250044127.jpg",
                    Priority = 2,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/250044109.jpg",
                    Priority = 3,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/250044106.jpg",
                    Priority = 4,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/250044088.jpg",
                    Priority = 5,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(SamsungOdysseyphoto1);
                dataContext.ProductImages.Add(SamsungOdysseyphoto2);
                dataContext.ProductImages.Add(SamsungOdysseyphoto3);
                dataContext.ProductImages.Add(SamsungOdysseyphoto4);
                dataContext.ProductImages.Add(SamsungOdysseyphoto5);
                #endregion
                #region Asus TUF Gaming VG259QR (90LM0530-B03370) 8-Bit / 165Hz / Adaptive-Sync / G-Sync Compatible
                var AsusTUF = products.Where(x => x.Name == "Asus TUF Gaming VG259QR (90LM0530-B03370) 8-Bit / 165Hz / Adaptive-Sync / G-Sync Compatible").First().Id;

                var AsusTUFphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/168721089.jpg",
                    Priority = 1,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/168721090.jpg",
                    Priority = 2,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/168721091.jpg",
                    Priority = 3,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/168721092.jpg",
                    Priority = 4,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/168721093.jpg",
                    Priority = 5,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AsusTUFphoto1);
                dataContext.ProductImages.Add(AsusTUFphoto2);
                dataContext.ProductImages.Add(AsusTUFphoto3);
                dataContext.ProductImages.Add(AsusTUFphoto4);
                dataContext.ProductImages.Add(AsusTUFphoto5);
                #endregion
                #endregion
                #region Мобільні телефони
                #region Apple iPhone 13 128GB Pink (MLPH3HU/A)
                var AppleiPhone13 = products.Where(x => x.Name == "Apple iPhone 13 128GB Pink (MLPH3HU/A)").First().Id;

                var AppleiPhone13photo1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/221214151.jpg",
                    Priority = 1,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/221023395.jpg",
                    Priority = 2,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/221214152.jpg",
                    Priority = 3,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/221214153.jpg",
                    Priority = 4,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/221214154.jpg",
                    Priority = 5,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AppleiPhone13photo1);
                dataContext.ProductImages.Add(AppleiPhone13photo2);
                dataContext.ProductImages.Add(AppleiPhone13photo3);
                dataContext.ProductImages.Add(AppleiPhone13photo4);
                dataContext.ProductImages.Add(AppleiPhone13photo5);
                #endregion
                #endregion
                #region Телевізори
                #region Samsung UE50AU7100UXUA
                var Samsung = products.Where(x => x.Name == "Samsung UE50AU7100UXUA").First().Id;

                var Samsungphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/303985202.jpg",
                    Priority = 1,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/303985203.jpg",
                    Priority = 2,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/303985204.jpg",
                    Priority = 3,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/303985205.jpg",
                    Priority = 4,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/303985206.jpg",
                    Priority = 5,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(Samsungphoto1);
                dataContext.ProductImages.Add(Samsungphoto2);
                dataContext.ProductImages.Add(Samsungphoto3);
                dataContext.ProductImages.Add(Samsungphoto4);
                dataContext.ProductImages.Add(Samsungphoto5);
                #endregion
                #endregion

                dataContext.SaveChanges();
            }
        }
    }
}
