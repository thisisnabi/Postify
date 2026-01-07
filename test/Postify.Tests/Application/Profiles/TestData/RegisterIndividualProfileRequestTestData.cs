using AutoFixture;
using AutoFixture.Dsl;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Profile.Tests.Common.TestData;

namespace Postify.Profile.Tests.Application.Profiles.TestData;

internal class RegisterIndividualProfileRequestTestData : BaseTestData
{
    private IPostprocessComposer<RegisterIndividualProfileRequest> _registerIndividualProfileRequestFixture;

    public RegisterIndividualProfileRequestTestData()
    {
        _registerIndividualProfileRequestFixture = Fixture.Build<RegisterIndividualProfileRequest>();
    }

    public RegisterIndividualProfileRequest Create()
    {
        return _registerIndividualProfileRequestFixture.Create();
    }

    public RegisterIndividualProfileRequestTestData WithNationalId(string nationalId)
    {
        _registerIndividualProfileRequestFixture =
            _registerIndividualProfileRequestFixture.With(request => request.NationalId, nationalId);

        return this;
    }

    public RegisterIndividualProfileRequestTestData WithFirstName(int length)
    {
        var firstName = new string('X', length);
        _registerIndividualProfileRequestFixture =
            _registerIndividualProfileRequestFixture.With(request => request.FirstName, firstName);

        return this;
    }

    public RegisterIndividualProfileRequestTestData WithLastName(int length)
    {
        var lastName = new string('X', length);
        _registerIndividualProfileRequestFixture =
            _registerIndividualProfileRequestFixture.With(request => request.LastName, lastName);

        return this;
    }

    public RegisterIndividualProfileRequestTestData WithPhoneNumber(int length)
    {
        var phoneNumber = new string('1', length);
        _registerIndividualProfileRequestFixture =
            _registerIndividualProfileRequestFixture.With(request => request.PhoneNumber, phoneNumber);

        return this;
    }
}

