using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.CategrotyAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CategrotyAgg
{
    public class Category : AggregateRoot
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public long? ParentId { get; private set; }
        public List<Category> Childs { get; private set; }
        private Category()
        {
        }

        public Category(string title, string slug, SeoData seoData, ICategoryDomainServices categoryDomainServices)
        {
            slug = slug?.ToString();
            Guard(title, slug, categoryDomainServices);
            Slug = slug.ToString();
            Title=title;
            SeoData=seoData;
        }

        public void Edit(string title, string slug, SeoData seoData, ICategoryDomainServices categoryDomainServices)
        {  
            slug = slug?.ToString();
            Guard(title, slug, categoryDomainServices);
            Title =title;
            Slug=slug;
            SeoData=seoData;
        }

        public void Guard(string title, string slug, ICategoryDomainServices categoryDomainServices)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
            if(slug != Slug)
            if (categoryDomainServices.IsSlugExist(slug))
                throw new SlugIsDuplicateException("This Slug Is Exist");
        }
 
        public void AddChild(string title, string slug, SeoData seoData , ICategoryDomainServices categoryDomainServices)
        {
            Childs.Add(new Category(title, slug, seoData, categoryDomainServices) 
            { 
                ParentId = this.Id 
            });
        }
    }
}
