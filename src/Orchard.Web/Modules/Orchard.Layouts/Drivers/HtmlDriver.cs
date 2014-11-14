﻿using Orchard.Layouts.Elements;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Services;
using Orchard.Layouts.ViewModels;

namespace Orchard.Layouts.Drivers {
    public class HtmlDriver : ElementDriver<Html> {

        protected override EditorResult OnBuildEditor(Html element, ElementEditorContext context) {
            var viewModel = new HtmlEditorViewModel {
                Text = element.Content
            };
            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Html", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Content = viewModel.Text;
            }
            
            return Editor(context, editor);
        }

        protected override void OnIndexing(Html element, ElementIndexingContext context) {
            context.DocumentIndex
                .Add("body", element.Content).RemoveTags().Analyze()
                .Add("format", "html").Store();
        }

        protected override void OnBuildDocument(Html element, BuildElementDocumentContext context) {
            context.HtmlContent = element.Content;
        }
    }
}