using System.Collections.Generic;
using System.Windows.Forms;
using Logic;

namespace Logic.Service.Validation
{
    public class Validator
    {
        public static bool CheckUrlContainsRssOrXml(string name)
        {
            if (!name.Contains("rss") && !name.Contains("xml"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckIfNameIsValid(string name)
        {
            if (name.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckIfCategoryAlreadyExists(string category, string newCategory)
        {
            if (newCategory.Equals(category))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public static bool CheckIfNameAlreadyExists(string name, List<string> secondName)
        //{
        //    for (int i = 0; i < secondName.Length; i++)
        //    {
        //        foreach (var item in secondName)
        //        {
        //            if (secondName.Equals(name))
        //            {
        //                return false;
        //                MessageBox.Show("Kategorin finns redan!");
        //            }
        //        }
        //    }
        //    return true;
           
        //}
        


    }
}
