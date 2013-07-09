using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ByteCarrot.Masslog.Core.Infrastructure;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public static class FieldContainerExtensions
    {
        private static MvcHtmlString CreateFieldContainer(Action<TagBuilder> action)
        {
            var container = new TagBuilder("div");
            container.AddCssClass("field-container");
            action(container);
            return MvcHtmlString.Create(container.ToString(TagRenderMode.Normal));
        }

        private static MvcHtmlString LabelForField<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {
            var propertyName = MvcTools.GetName(helper, expression);
            var labelText = propertyName;
            var propertyMetadata = helper.ViewData.ModelMetadata.Properties.SingleOrDefault(y => y.PropertyName == propertyName);
            if (propertyMetadata != null && !propertyMetadata.DisplayName.Empty())
            {
                labelText = propertyMetadata.DisplayName;
            }
            
            var label = new TagBuilder("label");
            label.Attributes.Add("for", propertyName);
            label.SetInnerText(helper.Encode(labelText));
            label.AddCssClass("checkbox");

            return MvcHtmlString.Create(label.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString TextBoxFieldFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return CreateFieldContainer(x =>
            {
                x.InnerHtml += helper.LabelFor(expression);
                x.InnerHtml += helper.ValidationTooltipFor(expression);
                x.InnerHtml += helper.TextBoxFor(expression);
            });
        }

        public static MvcHtmlString RulesEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return CreateFieldContainer(x =>
            {
                x.InnerHtml += helper.TextAreaFor(expression);
                x.InnerHtml += helper.RenderParseError(Name.Of(expression));
            });
        }

        public static MvcHtmlString RenderParseError(this HtmlHelper helper, string name)
        {
            var span = new TagBuilder("span");

            var div1 = new TagBuilder("div");
            div1.AddCssClass("field-parse-error");
            div1.Attributes.Add("data-valmsg-for", name);

            var div2 = new TagBuilder("div");
            div2.AddCssClass("field-parse-error-pointer");

            div1.InnerHtml += span.ToString(TagRenderMode.Normal);
            div1.InnerHtml += div2.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(div1.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString PasswordFieldFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return CreateFieldContainer(x =>
            {
                x.InnerHtml += helper.LabelFor(expression);
                x.InnerHtml += helper.ValidationTooltipFor(expression);
                x.InnerHtml += helper.PasswordFor(expression);
            });
        }

        public static MvcHtmlString CheckboxFieldFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {
            return CreateFieldContainer(x =>
            {
                x.InnerHtml += helper.CheckBoxFor(expression);
                x.InnerHtml += helper.LabelForField(expression);
            });
        }

        public static MvcHtmlString ReadOnlyFieldFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var propertyName = MvcTools.GetName(helper, expression);

            var model = helper.ViewData.Model;
            var type = model.GetType();
            var text = type.GetProperty(propertyName).GetValue(model, null) as string;

            return CreateFieldContainer(x =>
            {
                x.InnerHtml += helper.LabelFor(expression);

                var span = new TagBuilder("span");
                span.Attributes.Add("id", propertyName);
                span.Attributes.Add("class", "readonly");
                span.SetInnerText(helper.Encode(text));
                x.InnerHtml += MvcHtmlString.Create(span.ToString(TagRenderMode.Normal));
                x.InnerHtml += helper.HiddenFor(expression);
            });
        }
    }
}