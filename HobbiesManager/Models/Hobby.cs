using HobbiesManager.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbiesManager.Models
{
    public class Hobby
    {
        public string Name { get; set; }
        public HobbyCategory Category { get; set; }
        public string ImageURL { get; set; }
        public ObservableCollection<string> Equipment { get; set; }

        public string GetCategoryDescription()
        {
            var fieldInfo = Category.GetType().GetField(Category.ToString());
            if (fieldInfo != null)
            {
                var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>().FirstOrDefault();
                return descriptionAttribute?.Description ?? Category.ToString();
            }
            return Category.ToString();
        }

        public static HobbyCategory GetCategoryFromDescription(string description)
        {
            foreach (var category in Enum.GetValues(typeof(HobbyCategory)).Cast<HobbyCategory>())
            {
                var fieldInfo = category.GetType().GetField(category.ToString());
                var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>().FirstOrDefault();
                var categoryDescription = descriptionAttribute?.Description ?? category.ToString();

                if (categoryDescription == description)
                {
                    return category;
                }
            }
            return HobbyCategory.General; 
        }
    }

}

