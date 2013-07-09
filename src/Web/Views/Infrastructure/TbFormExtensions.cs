using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public static class TbFormExtensions
    {
        private static IHtmlString ControlGroup<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, Action<TagBuilder> action)
        {
            var propertyName = MvcTools.GetName(helper, expression);
            var message = MvcTools.GetMessage(helper, propertyName);

            var containerDiv = new TagBuilder("div");
            containerDiv.AddCssClass("control-group");
            containerDiv.GenerateId(propertyName + "ControlGroup");
            if (message != null)
            {
                containerDiv.AddCssClass("error");
            }

            var label = new TagBuilder("label");
            label.AddCssClass("control-label");
            label.MergeAttribute("for", propertyName);
            label.SetInnerText(MvcTools.GetDisplayName(helper, propertyName));

            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("controls");

            var span = new TagBuilder("span");
            span.AddCssClass("help-block");
            span.SetInnerText(message);
            action(innerDiv);
            innerDiv.InnerHtml += span;
            containerDiv.InnerHtml = label.ToString() + innerDiv;

            return new MvcHtmlString(containerDiv.ToString());
        }

        public static IHtmlString TbPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return ControlGroup(helper, expression, tb =>
            {
                tb.InnerHtml += helper.PasswordFor(expression);
            });
        }

        public static IHtmlString TbTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return ControlGroup(helper, expression, tb =>
            {
                tb.InnerHtml += helper.TextBoxFor(expression);
            });
        }

        public static IHtmlString TbCheckBoxFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {
            return ControlGroup(helper, expression, tb =>
            {
                tb.InnerHtml += helper.CheckBoxFor(expression);
            });
        }

        public static IHtmlString TbDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return ControlGroup(helper, expression, tb =>
            {
                tb.InnerHtml += helper.DropDownListFor(expression, selectList);
            });
        }

        public static IHtmlString TbUneditableFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return ControlGroup(helper, expression, tb =>
            {
                var value = MvcTools.GetValue(helper, expression);

                var span = new TagBuilder("span");

                var @checked = false;
                if (Boolean.TryParse(value, out @checked))
                {
                    span.AddCssClass(@checked ? "icon-ok" : "icon-ban-circle");
                    span.AddCssClass("uneditable-checkbox");
                }
                else
                {
                    span.AddCssClass("uneditable-input");
                    span.SetInnerText(value);
                }
                tb.InnerHtml += span.ToString();
                tb.InnerHtml += helper.HiddenFor(expression);
            });
        }
    }
}