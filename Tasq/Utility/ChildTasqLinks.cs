using Contracts;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasq.Utility
{
    public class ChildTasqLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<TasqDto> _dataShaper;

        public ChildTasqLinks(LinkGenerator linkGenerator, IDataShaper<TasqDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<TasqDto> tasqsDto, string fields, Guid tasqId, HttpContext httpContext)
        {
            var shapedTasqs = ShapeData(tasqsDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedTasqs(tasqsDto, fields, tasqId, httpContext, shapedTasqs);

            return ReturnShapedTasqs(shapedTasqs);
        }

        private List<Entity> ShapeData(IEnumerable<TasqDto> tasqsDto, string fields) =>
            _dataShaper.ShapeData(tasqsDto, fields)
            .Select(t => t.Entity)
            .ToList();

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedTasqs(List<Entity> shapedTasqs) => new LinkResponse { ShapedEntities = shapedTasqs };

        private LinkResponse ReturnLinkedTasqs(IEnumerable<TasqDto> tasqsDto, string fields, Guid tasqId, HttpContext httpContext, List<Entity> shapedTasqs)
        {
            var tasqDtoList = tasqsDto.ToList();

            for (var index = 0; index < tasqDtoList.Count(); index++)
            {
                var tasqLinks = CreateLinksForTasq(httpContext, tasqId, tasqDtoList[index].Id, fields);
                shapedTasqs[index].Add("Links", tasqLinks);
            }

            var tasqCollection = new LinkCollectionWrapper<Entity>(shapedTasqs);
            var linkedTasqs = CreateLinksForTasqs(httpContext, tasqCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedTasqs };
        }

        private List<Link> CreateLinksForTasq(HttpContext httpContext, Guid tasqId, Guid childId, string fields = "")
        {
            var links = new List<Link>
            { new Link(_linkGenerator.GetUriByAction(httpContext, "GetChildForTasq", values: new {tasqId, childId, fields }),
            "self",
            "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteChildTasq", values: new {tasqId, childId }),
            "delete_tasq",
            "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateChildTasq", values: new {tasqId, childId }),
            "update_tasq",
            "PUT"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateChildTasq", values: new {tasqId, childId }),
            "partially_update_tasq",
            "PATCH")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForTasqs(HttpContext httpContext, LinkCollectionWrapper<Entity> tasqsWrapper)
        {
            tasqsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetChildrenForTasq", values: new { }),
                "self",
                "GET"));

            return tasqsWrapper;
        }
    }
}
