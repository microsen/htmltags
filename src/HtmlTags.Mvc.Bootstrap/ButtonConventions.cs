namespace HtmlTags.Mvc.Bootstrap
{
    internal class ButtonConventions : ElementConventionLibrary
    {
        public ButtonConventions()
        {
            If(x => x.GetData<bool>("button"))
                .Modify(x =>
                        {
                            x.OriginalTag.AddClass("btn");
                            if (x.HasData("buttonType"))
                                x.OriginalTag.AddClass("btn-" + x.GetData<ButtonType>("buttonType").ToString().ToLower());

                            if (!x.HasData("buttonSize")) return;
                            var size = x.GetData<ButtonSize>("buttonSize");
                            if(size != ButtonSize.Default)
                                x.OriginalTag.AddClass("btn-" + size.ToString().ToLower());
                        });
        }
    }
}