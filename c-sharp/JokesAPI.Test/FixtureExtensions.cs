using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;

namespace JokesAPI.Test
{
    public static class FixtureExtensions
    {
        public static int CreateInt(this IFixture fixture, int min, int max)
        {
            return fixture.Create<int>() % (max - min + 1) + min;
        }
    }
}
