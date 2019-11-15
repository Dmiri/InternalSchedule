using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Hnatob.WebUI.Models.Helpers
{
    public static class ValidatorsRegex
    {
        public static string patternText = @"(^[\w\s-+_.,;:?()]*$)";
        public static string patternPhone = @"(\+?[0-9]{3}-*[0-9]{2}-*[0-9]{3}-*[0-9]{2}-*[0-9]{2})";
        public static string patternEmail = @"(^(\w[-._+\w]*\w@\w[-._\w]*\w\.\w{2,4})$)";
        public static int maxShortLenght = 32;
        public static int maxMediumLenght = 32;
        public static int maxLongLenght = 32;
    }
}