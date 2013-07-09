using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public static class MvcTools
    {
        public static string GetName<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var expressionString = ExpressionHelper.GetExpressionText(expression);
            return helper.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionString);
        }

        public static string GetDisplayName<TModel>(HtmlHelper<TModel> helper, string name)
        {
            var displayName = name;
            var propertyMetadata = helper.ViewData.ModelMetadata.Properties.SingleOrDefault(y => y.PropertyName == name);
            if (propertyMetadata != null && !propertyMetadata.DisplayName.Empty())
            {
                displayName = propertyMetadata.DisplayName;
            }
            return displayName;
        }

        public static string GetMessage<TModel>(HtmlHelper<TModel> helper, string name)
        {
            var model = helper.ViewData.ModelState[name];
            if (model == null)
            {
                return null;
            }

            if (model.Errors == null || model.Errors.Count == 0)
            {
                return null;
            }

            return model.Errors[0].ErrorMessage;
        }

        public static string GetValue<TModel>(HtmlHelper<TModel> helper, string propertyName)
        {
            var model = helper.ViewData.Model;
            var type = model.GetType();
            return type.GetProperty(propertyName).GetValue(model, null).ToString();
        }

        public static string GetValue<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var model = helper.ViewData.Model;
            var type = model.GetType();
            var value = type.GetProperty(GetName(helper, expression)).GetValue(model, null);
            return value == null ? null : value.ToString();
        }
    }
}