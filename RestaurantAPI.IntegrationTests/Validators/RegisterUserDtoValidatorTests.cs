﻿using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entites;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Validators;
using System.Collections.Generic;
using Xunit;

namespace RestaurantAPI.IntegrationTests.Validators
{
    public class RegisterUserDtoValidatorTests
    {
        private RestaurantDbContext _dbContext;

        public RegisterUserDtoValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase("TestDb");

            _dbContext = new RestaurantDbContext(builder.Options);
            Seed();
        }

        public void Seed()
        {
            var testUsers = new List<User>()
            {
                new User()
                {
                    Email = "test2@test.com"
                },
                new User()
                {
                    Email = "test3@test.com"
                }
            };

            _dbContext.Users.AddRange(testUsers);
            _dbContext.SaveChanges();
        }
        [Fact]
        public void Validate_ForValidModel_ReturnSuccess()
        {
            //arrange

            var model = new RegisterUserDto()
            {
                Email = "test@test.com",
                Password = "pass123",
                ConfirmPassword = "pass123"
            };

            var validator = new RegisterUserDtoValidator(_dbContext);

            //act

            var result = validator.TestValidate(model);

            //assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ForInvalidModel_ReturnFailure()
        {
            //arrange

            var model = new RegisterUserDto()
            {
                Email = "test2@test.com",
                Password = "pass123",
                ConfirmPassword = "pass123"
            };

            var validator = new RegisterUserDtoValidator(_dbContext);

            //act

            var result = validator.TestValidate(model);

            //assert

            result.ShouldHaveAnyValidationError();
        }
    }
}
