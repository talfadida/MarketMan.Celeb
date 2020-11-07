using HtmlAgilityPack;
using MarketMan.Celeb.Business;
using MarketMan.Celeb.Business.Core;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MarketMan.Celeb.UnitTest
{
    [TestClass]
    public class HappyFlowUnitTest
    {
        [TestMethod]
        public void CelebJsonFileRepository_Add_Item()
        {
            //var mockLogger = new Mock<ILogger<CelebJsonFileRepository>>();
            //IRepository repo = new CelebJsonFileRepository(mockLogger.Object);
            //repo.Add(new Entities.CelebInfo() { Key = 1 });
            //Assert.IsTrue(repo.GetAll().Result.Count == 1); 
        }


        [TestMethod]
        public void ImdbScrapEngine_Create_CelebObject()
        {
            //var mockRepoLogger = new Mock<ILogger<CelebJsonFileRepository>>();
            //var mockScrapLogger = new Mock<ILogger<ImdbScrapEngine>>();
            //IRepository repo = new CelebJsonFileRepository(mockRepoLogger.Object);
            //Mock<ImdbScrapEngine> engine = new Mock<ImdbScrapEngine>(repo, mockScrapLogger.Object);

            //var idmb_celeb_html = @"<div class=""lister-item mode-detail"">
            //                                <div class=""lister-item-image"">
            //                        <a href=""/name/nm0000136/?ref_=nmls_pst""> <img alt=""Johnny Depp"" height=""209"" src=""https://m.media-amazon.com/images/M/MV5BMTM0ODU5Nzk2OV5BMl5BanBnXkFtZTcwMzI2ODgyNQ@@._V1_UY209_CR3,0,140,209_AL_.jpg"" width=""140"">
            //                        </a>        </div>
            //                                <div class=""lister-item-content"">
            //                                    <h3 class=""lister-item-header"">
            //                                        <span class=""lister-item-index unbold text-primary"">1. </span>
            //                        <a href=""/name/nm0000136?ref_=nmls_hd""> Johnny Depp
            //                        </a>            </h3>
            //                                        <p class=""text-muted text-small"">
            //                                                Actor <span class=""ghost"">|</span>
            //                        <a href=""/title/tt0408236/?ref_=nmls_kf""> Sweeney Todd: The Demon Barber of Fleet Street
            //                        </a>                </p>
            //                                        <p>
            //                            Johnny Depp is perhaps one of the most versatile actors of his day and age in Hollywood.<br><br>He was born John Christopher Depp II in Owensboro, Kentucky, on June 9, 1963, to Betty Sue (Wells), who worked as a waitress, and John Christopher Depp, a civil engineer.<br><br>Depp was raised in Florida. He dropped ...                </p>
            //                                </div>
            //                                <div class=""clear""></div>
            //                            </div>";
            //HtmlNode mockNode = HtmlNode.CreateNode(idmb_celeb_html);

            //engine.Setup(x => x.GetBirthDateFromInnerWebCall(mockNode)).Returns("2001-01-01");
           
           
            //var celebObject = engine.Object.Create(mockNode);
            //Assert.IsNotNull(celebObject);

            //Assert.AreSame(celebObject.Name, "Johnny Depp");
            //Assert.AreSame(celebObject.Role, "Actor");
            //Assert.AreSame(celebObject.ImageUrl, "https://m.media-amazon.com/images/M/MV5BMTM0ODU5Nzk2OV5BMl5BanBnXkFtZTcwMzI2ODgyNQ@@._V1_UY209_CR3,0,140,209_AL_.jpg");
            //Assert.AreSame(celebObject.Gender, "Male");
            //Assert.AreSame(celebObject.BirthDate, "2001-01-01"); //we are not testing; its mock value
        }

    }


}
