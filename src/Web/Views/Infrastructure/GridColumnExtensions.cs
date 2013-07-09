using MvcContrib.UI.Grid;

namespace ByteCarrot.Masslog.Web.Views.Infrastructure
{
    public static class GridColumnExtensions
    {
        public static IGridColumn<T> TextColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "text-column nowrap-text");
        }

        public static IGridColumn<T> LongTextColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "text-column wrap-text");
        }

        public static IGridColumn<T> NumericColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "numeric-column nowrap-text");
        }

        public static IGridColumn<T> IdColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "id-column nowrap-text");
        }

        public static IGridColumn<T> TimeColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "time-column nowrap-text");
        }

        public static IGridColumn<T> ActionsColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "actions-column");
        }

        public static IGridColumn<T> BooleanColumn<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "boolean-column");
        }

        public static IGridColumn<T> DesktopOnly<T>(this IGridColumn<T> c)
        {
            return c.Attributes(@class => "visible-desktop").HeaderAttributes(@class => "visible-desktop");
        }
    }
}