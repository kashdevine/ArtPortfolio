using ArtPortfolio.Contracts;
using ArtPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArtPortfolio.Tests.JWTests
{
    public class JWTServiceTest
    {
        private IJWTService _sut;

        public JWTServiceTest()
        {
            _sut = new JWTService();
        }
    }
}
