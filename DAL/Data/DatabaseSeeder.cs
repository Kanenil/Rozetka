using DAL.Data.Entities;
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

                var planshety = new CategoryEntity
                {
                    Name = "Планшети",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/planshety.png"
                };
                
                var klaviatury = new CategoryEntity
                {
                    Name = "Клавіатури та мишки",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/klaviatury-i-myshi.png"
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
                dataContext.Categories.Add(planshety);
                dataContext.Categories.Add(klaviatury);
                dataContext.Categories.Add(mobilnye_telefony);
                dataContext.Categories.Add(televizory);
                dataContext.SaveChanges();
            }
        }

    }
}
