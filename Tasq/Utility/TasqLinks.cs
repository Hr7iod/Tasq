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
    public class TasqLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<TasqDto> _dataShaper;

        public TasqLinks(LinkGenerator linkGenerator, IDataShaper<TasqDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<TasqDto> tasqsDto, string fields, HttpContext httpContext)
        {
            var shapedTasqs = ShapeData(tasqsDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedTasqs(tasqsDto, fields, httpContext, shapedTasqs);

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

        private LinkResponse ReturnLinkedTasqs(IEnumerable<TasqDto> tasqsDto, string fields, HttpContext httpContext, List<Entity> shapedTasqs)
        {
            var tasqDtoList = tasqsDto.ToList();

            for (var index = 0; index < tasqDtoList.Count(); index++)
            {
                var tasqLinks = CreateLinksForTasq(httpContext, tasqDtoList[index].Id, fields);
                shapedTasqs[index].Add("Links", tasqLinks);
            }

            var tasqCollection = new LinkCollectionWrapper<Entity>(shapedTasqs);
            var linkedTasqs = CreateLinksForTasqs(httpContext, tasqCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedTasqs };
        }

        private List<Link> CreateLinksForTasq(HttpContext httpContext, Guid id, string fields = "")
        {
            var links = new List<Link>
            { new Link(_linkGenerator.GetUriByAction(httpContext, "GetTasq", values: new {id, fields}),
            "self",
            "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteTasq", values: new {id}),
            "delete_tasq",
            "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateTasq", values: new {id}),
            "update_tasq",
            "PUT"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "PatriallyUpdateTasq", values: new {id}),
            "partially_update_tasq",
            "PATCH")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForTasqs(HttpContext httpContext, LinkCollectionWrapper<Entity> tasqsWrapper)
        {
            tasqsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetTasqs", values: new { }),
                "self",
                "GET"));

            return tasqsWrapper;
        }
    }
}
