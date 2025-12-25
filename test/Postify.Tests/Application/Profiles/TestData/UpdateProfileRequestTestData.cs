using AutoFixture;
using AutoFixture.Dsl;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Profile.Tests.Common.TestData;

namespace Postify.Profile.Tests.Application.Profiles.TestData;

internal class UpdateProfileRequestTestData : BaseTestData
{
    private IPostprocessComposer<UpdateProfileRequest> _updateProfileRequestFixture;

    public UpdateProfileRequestTestData()
    {
        _updateProfileRequestFixture = Fixture.Build<UpdateProfileRequest>()
            .Without(request => request.FirstName)
            .Without(request => request.LastName)
            .Without(request => request.PhoneNumber)
            .Without(request => request.Name)
            .Without(request => request.EconomicId);
    }

    public UpdateProfileRequest Create()
    {
        return _updateProfileRequestFixture.Create();
    }

    public UpdateProfileRequestTestData WithFirstName(int length)
    {
        var firstName = new string('X', length);
        _updateProfileRequestFixture =
            _updateProfileRequestFixture.With(request => request.FirstName, firstName);

        return this;
    }

    public UpdateProfileRequestTestData WithLastName(int length)
    {
        var lastName = new string('X', length);
        _updateProfileRequestFixture =
            _updateProfileRequestFixture.With(request => request.LastName, lastName);

        return this;
    }

    public UpdateProfileRequestTestData WithPhoneNumber(int length)
    {
        var phoneNumber = new string('1', length);
        _updateProfileRequestFixture =
            _updateProfileRequestFixture.With(request => request.PhoneNumber, phoneNumber);

        return this;
    }

    public UpdateProfileRequestTestData WithEconomicId(string economicId)
    {
        _updateProfileRequestFixture =
            _updateProfileRequestFixture.With(request => request.EconomicId, economicId);

        return this;
    }
}

