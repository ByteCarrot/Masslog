using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public static class ValidationExtensions
    {
        public static MvcHtmlString ValidationTooltipFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = MvcTools.GetName(helper, expression);
            var message = GetMessage(helper, name);
            return message != null 
                ? RenderErrorMessage(helper, name, message) 
                : null;
        }

        public static MvcHtmlString RenderErrorMessage(this HtmlHelper helper, string name, string message)
        {
            var div1 = new TagBuilder("div");
            div1.AddCssClass("field-validation-error");
            div1.Attributes.Add("data-valmsg-for", name);
            div1.InnerHtml += helper.Encode(message);

            var div2 = new TagBuilder("div");
            div2.AddCssClass("field-validation-error-pointer");
            div1.InnerHtml += div2.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(div1.ToString(TagRenderMode.Normal));
        }

        private static string GetMessage<TModel>(HtmlHelper<TModel> helper, string name)
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
    }
}