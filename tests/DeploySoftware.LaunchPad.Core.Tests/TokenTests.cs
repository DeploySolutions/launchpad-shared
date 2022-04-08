//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Core.Tests
{
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using DeploySoftware.LaunchPad.FileGeneration.Stages;

    public class TokenTests : IClassFixture<TokenTestsFixture>
    {
        #region "Test Classes"



        #endregion

        private readonly TokenTestsFixture _fixture;
        public TokenTests(TokenTestsFixture fixture)
        {
            this._fixture = fixture;
            var token = new LaunchPadToken();
            this._fixture.Initialize(token);
        }

        [Fact]
        public void Should_Have_NotNull_Prefix_When_Instantiated()
        {
            _fixture.SUT.Prefix.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_DefaultValue_When_Instantiated()
        {
            _fixture.SUT.DefaultValue.Should().NotBeNull();
        }
        
        [Fact]
        public void Should_Have_NotNull_Value_When_Instantiated()
        {
            _fixture.SUT.Value.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_Name_When_Instantiated()
        {
            _fixture.SUT.Name.Should().NotBeNull();
        }

        [Fact]
        public void Should_GuardAgainst_Invalid_TokenString()
        {
            Assert.Throws<ArgumentException>(() => new LaunchPadToken("invalid_token_string"));
        }

        [Fact]
        public void Should_GuardAgainst_TokenString_Missing_Prefix_Section()
        {
            var ex = Record.Exception(() => new LaunchPadToken("{{dss|n:domain_entity_name}}"));
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains("Token string must contain a prefix section, beginning with 'p:'.", ex.Message);
        }

        [Fact]
        public void Should_GuardAgainst_TokenString_Missing_Name_Section()
        {
            var ex = Record.Exception(() => new LaunchPadToken("{{p:dss|domain_entity_name}}"));
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains("Token string must contain a name section, beginning with 'n:'.", ex.Message);
        }

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

        [Fact]
        public void Should_Match_TokenWithTags()
        {
            string originalText = @"{{p:dss|n:domain_namespace|tags:a=x;b=y;}} // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;}}";
            string expectedResult = @"DeploySoftware.LaunchPad.Core.Tests // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;}}";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Tags.Add("a", "x");
            token.Tags.Add("b", "y");
            token.Value = "DeploySoftware.LaunchPad.Core.Tests";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex, act);

        }

        [Fact]
        public void Should_Match_TokenWith_Value_And_Tags()
        {
            string originalText = @"{{p:dss|n:domain_namespace|tags:a=x;b=y;|v:DeploySoftware.LaunchPad.Core.Tests}} // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            string expectedResult = @"DeploySoftware.LaunchPad.Core.Tests // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Tags.Add("a", "x");
            token.Tags.Add("b", "y");
            token.Value = "DeploySoftware.LaunchPad.Core.Tests";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens,true);
            string ex = expectedResult.Trim().Replace("\r\n", string.Empty);
            string act = tokenizer.TokenizedText.Trim().Replace("\r\n", string.Empty);
            Assert.Equal(ex, act);
        }

        [Fact]
        public void Should_Match_TokenWith_DefaultValue_And_Tags()
        {
            string originalText = @"{{p:dss|n:domain_namespace|tags:a=x;b=y;|dv:DeploySoftware.LaunchPad.Core.Tests}} // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            string expectedResult = @"DeploySoftware.LaunchPad.Core.Tests // should not change this: {{p:dss|n:domain_namespace|tags:a=x;b=z;|v:no_match}}";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Tags.Add("a", "x");
            token.Tags.Add("b", "y");
            token.DefaultValue = "DeploySoftware.LaunchPad.Core.Tests";
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
