using System.Collections.Generic;
using System.Net.Http;
using HyperLibrary.Core.Controllers;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core.LibraryModel
{
    public class BookResourceMapper
    {
        private readonly IResourceLinker _resourceLinker;

        public BookResourceMapper(IResourceLinker resourceLinker)
        {
            _resourceLinker = resourceLinker;
        }

        public  BookResource MapToResouce(Book book)
        {
            BookResource resource = new BookResource();
            resource.Id = book.Id;
            resource.Title = book.Title;
            resource.Author = book.Author;
            resource.Description = book.Description;
            resource.Self = _resourceLinker.GetResourceLink<BooksController>(request => request.Get(book.Id), "self", book.Title, HttpMethod.Get);
            resource.Links = new List<Link>();
            if (book.IsCheckedOut)
            {
                var checkInLink = _resourceLinker.GetResourceLink<CheckInController>(request => request.Post(book.Id), "Check In", book.Title, HttpMethod.Post);
                resource.Links.Add(checkInLink);
            }

            if (book.IsCheckedIn)
            {
                var checkoutLink = _resourceLinker.GetResourceLink<CheckedOutController>(request => request.Post(book.Id), "Check Out", book.Title, HttpMethod.Post);
                resource.Links.Add(checkoutLink);
            }
            resource.Links.Add(_resourceLinker.GetResourceLink<RootController>(request => request.Get(),"Home","Go Home",HttpMethod.Get));
            return resource;
        }
    }
}