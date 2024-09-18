using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPlusSportTDD.Core
{
    public class ShoppingCartTests
    {
        // Test to check if an article is added to the cart,
        // and method should return the article that has been added
        [Fact]
        public void Given_ArticleAddedToCart_When_CheckingCart_Then_ReturnArticle()
        {
            var item = new AddToCartItem()
            {
                ArticleId = 42,
                Quantity = 5
            };

            
            var request = new AddToCartRequest()
            {
                Item = item
            };

            var manager = new ShoppingCarManager();

            AddToCartResponse response = manager.AddToCart(request);

            

        }
    }

}

