// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="TokenTests.cs" company="Deploy.LaunchPad.Core.Tests">
//     Copyright (c) Deploy Software Solutions, Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

namespace Deploy.LaunchPad.Core.Tests
{
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using Deploy.LaunchPad.FileGeneration.Stages;
    using Deploy.LaunchPad.Core.Util;

    /// <summary>
    /// Class TokenTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.TokenTestsFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.TokenTestsFixture}" />
    public partial class TokenTests : IClassFixture<TokenTestsFixture>
    {
        #region "Test Classes"



        #endregion

        /// <summary>
        /// The fixture
        /// </summary>
        private readonly TokenTestsFixture _fixture;
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public TokenTests(TokenTestsFixture fixture)
        {
            this._fixture = fixture;
            var token = new LaunchPadToken();
            this._fixture.Initialize(token);
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_Prefix_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_Prefix_When_Instantiated()
        {
            _fixture.SUT.Prefix.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_DefaultValue_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_DefaultValue_When_Instantiated()
        {
            _fixture.SUT.DefaultValue.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_Value_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_Value_When_Instantiated()
        {
            _fixture.SUT.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_Name_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_Name_When_Instantiated()
        {
            _fixture.SUT.Name.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method Should_GuardAgainst_Invalid_TokenString.
        /// </summary>
        [Fact]
        public void Should_GuardAgainst_Invalid_TokenString()
        {
            Assert.Throws<ArgumentException>(() => new LaunchPadToken("invalid_token_string"));
        }

        /// <summary>
        /// Defines the test method Should_GuardAgainst_TokenString_Missing_Prefix_Section.
        /// </summary>
        [Fact]
        public void Should_GuardAgainst_TokenString_Missing_Prefix_Section()
        {
            var ex = Record.Exception(() => new LaunchPadToken("{{dss|n:domain_entity_name}}"));
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains("Token string must contain a prefix section, beginning with 'p:'.", ex.Message);
        }

        /// <summary>
        /// Defines the test method Should_GuardAgainst_TokenString_Missing_Name_Section.
        /// </summary>
        [Fact]
        public void Should_GuardAgainst_TokenString_Missing_Name_Section()
        {
            var ex = Record.Exception(() => new LaunchPadToken("{{p:dss|domain_entity_name}}"));
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains("Token string must contain a name section, beginning with 'n:'.", ex.Message);
        }

        /// <summary>
        /// Defines the test method Should_Match_TokenWithValue.
        /// </summary>
        [Fact]
        public void Should_Match_TokenWithValue()
        {
            string originalText = @"{{p:dss|n:domain_namespace|v:value}}";
            string expectedResult = @"value";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Value = "value";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens, true);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex,act);

        }

        /// <summary>
        /// Defines the test method Should_Match_TokenWithDefaultValue.
        /// </summary>
        [Fact]
        public void Should_Match_TokenWithDefaultValue()
        {
            string originalText = @"{{p:dss|n:domain_namespace|dv:value}}";
            string expectedResult = @"value";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.DefaultValue = "value";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens, true);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex, act);

        }

        /// <summary>
        /// Defines the test method Should_Match_TokenWithTags.
        /// </summary>
        [Fact]
        public void Should_Match_TokenWithTags()
        {
            string originalText = @"{{p:dss|n:domain_namespace|tags:a=x;b=y;}} // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;}}";
            string expectedResult = @"Deploy.LaunchPad.Core.Tests // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;}}";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Tags.Add("a", "x");
            token.Tags.Add("b", "y");
            token.Value = "Deploy.LaunchPad.Core.Tests";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex, act);

        }

        /// <summary>
        /// Defines the test method Should_Match_TokenWith_Value_And_Tags.
        /// </summary>
        [Fact]
        public void Should_Match_TokenWith_Value_And_Tags()
        {
            string originalText = @"{{p:dss|n:domain_namespace|tags:a=x;b=y;|v:Deploy.LaunchPad.Core.Tests}} // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            string expectedResult = @"Deploy.LaunchPad.Core.Tests // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Tags.Add("a", "x");
            token.Tags.Add("b", "y");
            token.Value = "Deploy.LaunchPad.Core.Tests";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens,true);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex, act);
        }

        /// <summary>
        /// Defines the test method Should_Match_TokenWith_DefaultValue_And_Tags.
        /// </summary>
        [Fact]
        public void Should_Match_TokenWith_DefaultValue_And_Tags()
        {
            string originalText = @"{{p:dss|n:domain_namespace|tags:a=x;b=y;|dv:Deploy.LaunchPad.Core.Tests}} // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            string expectedResult = @"Deploy.LaunchPad.Core.Tests // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Tags.Add("a", "x");
            token.Tags.Add("b", "y");
            token.DefaultValue = "Deploy.LaunchPad.Core.Tests";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens, true);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex, act);
        }

    }
}
