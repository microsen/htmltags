using System.Linq;
using System.Web.Mvc;

namespace HtmlTags.Mvc.Bootstrap
{
    public class BootstrapConventions : ElementConventionLibrary
    {
        public BootstrapConventions()
        {
            EditorsAlways().Modify(x => x.WrapWith(new DivTag().AddClass("form-group")));

            EditorsIf(x => x.Required && x.ModelState != null)
                .Modify(ApplyValidation);

            EditorsIf(x => !x.IsType<bool>()).Modify(x => x.OriginalTag.AddClass("form-control"));

            Import(new ButtonConventions());
        }

        

        private static void ApplyValidation(ElementRequest request)
        {
            var formGroup = request.CurrentTag;
            var input = request.OriginalTag;
            var modelState = request.ModelState;

            formGroup.AddClass("has-feedback");

            if (!modelState.Errors.Any())
            {
                var modelValue = request.Value;
                if (modelValue == null) return;
                formGroup.AddClass("has-success");
                input.AddClass(HtmlHelper.ValidationInputValidCssClassName);
                input.Next = new Glyphicon("ok").AddClass("form-control-feedback");
            }
            else
            {
                formGroup.AddClass("has-error");
                input.AddClass(HtmlHelper.ValidationInputCssClassName);
                input.Next = new Glyphicon("remove").AddClass("form-control-feedback");
                formGroup.Children.Add(new ValidationMessage(modelState.Errors.First().ErrorMessage));
            }
        }
    }
}