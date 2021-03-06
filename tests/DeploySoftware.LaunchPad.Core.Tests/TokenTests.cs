﻿//LaunchPad Shared
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
    using DeploySoftware.LaunchPad.Core.FileGeneration;
    using System;
    using System.Collections.Generic;

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
        public void Should_Match_Tokens()
        {
            string originalText = @"using System.Collections.Generic;
                                    using System.ComponentModel.DataAnnotations.Schema;
                                    using DeploySoftware.LaunchPad.Core.Domain;

                                    namespace {{p:dss|n:domain_namespace}}
                                    {
                                        /// <summary>
                                        /// {{p:dss|n:domain_entity_description}}
                                        /// </summary>
                                        {{p:dss|n:domain_entity_annotations}}
                                        public class {{p:dss|n:domain_entity_name}} : {{p:dss|n:domain_entity_inherits_from|dv:IDomainEntity<TIdType>}}
                                        {
                                            /// <summary>
                                            /// Creates a default {{p:dss|n:domain_entity_name}} entity
                                            /// </summary>
                                            public {{p:dss|n:domain_entity_name}}() : base()
                                            {

                                            }
                                        }
                                    }";
            string expectedResult = @"using System.Collections.Generic;
                                    using System.ComponentModel.DataAnnotations.Schema;
                                    using DeploySoftware.LaunchPad.Core.Domain;

                                    namespace Deploy.DeploymentGateway
                                    {
                                        /// <summary>
                                        /// {{p:dss|n:domain_entity_description}}
                                        /// </summary>
                                        {{p:dss|n:domain_entity_annotations}}
                                        public class {{p:dss|n:domain_entity_name}} : {{p:dss|n:domain_entity_inherits_from|dv:IDomainEntity<TIdType>}}
                                        {
                                            /// <summary>
                                            /// Creates a default {{p:dss|n:domain_entity_name}} entity
                                            /// </summary>
                                            public {{p:dss|n:domain_entity_name}}() : base()
                                            {

                                            }
                                        }
                                    }";
            var token = new LaunchPadToken("{{p:dss|n:domain_namespace}}");
            token.Value = "Deploy.DeploymentGateway";
            IDictionary<string, LaunchPadToken> tokens = new Dictionary<string, LaunchPadToken>();
            tokens.Add(token.Name, token);
            LaunchPadTokenizer tokenizer = new LaunchPadTokenizer();
            tokenizer.Tokenize(originalText, tokens);
            Assert.Equal(expectedResult, tokenizer.TokenizedText);

        }

    }
}
