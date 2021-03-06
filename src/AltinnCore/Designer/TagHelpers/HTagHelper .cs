﻿using System.Threading.Tasks;
using AltinnCore.Common.Helpers;
using AltinnCore.ServiceLibrary;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AltinnCore.Designer.TagHelpers
{
    /// <summary>
    /// Tag helper targeting spans to replace their content with a resource text
    /// </summary>
    [HtmlTargetElement("h1", Attributes = AltinnTextKeyAttributeName)]
    [HtmlTargetElement("h2", Attributes = AltinnTextKeyAttributeName)]
    [HtmlTargetElement("h3", Attributes = AltinnTextKeyAttributeName)]
    [HtmlTargetElement("h4", Attributes = AltinnTextKeyAttributeName)]
    [HtmlTargetElement("h5", Attributes = AltinnTextKeyAttributeName)]
    [HtmlTargetElement("h6", Attributes = AltinnTextKeyAttributeName)]
    public class HTagHelper : TagHelper
    {
        private const string AltinnTextKeyAttributeName = "altinn-text";

        /// <summary>
        /// Initializes a new instance of the <see cref="HTagHelper"/> class.
        /// </summary>
        /// <param name="generator">The current html generator</param>
        public HTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        /// Gets the ordering index
        /// </summary>
        public override int Order
        {
            get
            {
                return -1000;
            }
        }

        /// <summary>
        /// Gets or sets the resource text key to use when replacing content
        /// </summary>
        [HtmlAttributeName(AltinnTextKeyAttributeName)]
        public string AltinnTextKey { get; set; }
        
        /// <summary>
        /// Gets or sets the current view context (injected)
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        /// <summary>
        /// Gets the current html generator
        /// </summary>
        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// Method which processes the element and updates the content
        /// if all conditions are met and the given parameters are valid
        /// </summary>
        /// <param name="context">The current tag helper context</param>
        /// <param name="output">The output of this tag helper, the result is written here</param>
        /// <returns>Task indicating when the processing is completed</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = await output.GetChildContentAsync();
            ServiceContext serviceContext = ViewContext.ViewBag.ServiceContext;
            RequestContext requestContext = ViewContext.ViewBag.RequestContext;

            if (!string.IsNullOrEmpty(AltinnTextKey))
            {
                if (serviceContext.ServiceText.ContainsKey(AltinnTextKey)
                    && serviceContext.ServiceText[AltinnTextKey].ContainsKey(serviceContext.CurrentCulture))
                {
                    output.Content.SetHtmlContent(ServiceTextHelper.SetTextParams(serviceContext.ServiceText[AltinnTextKey][serviceContext.CurrentCulture], requestContext, serviceContext));
                }
            }
        }
    }
}
    