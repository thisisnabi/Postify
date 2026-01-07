using AutoFixture;
using AutoFixture.Dsl;
using Postify.Modules.Profile.Core.Payloads;
using Postify.Profile.Tests.Common.TestData;

namespace Postify.Profile.Tests.Application.Profiles.TestData;

internal class RegisterCorporateProfileRequestTestData : BaseTestData
{
    private IPostprocessComposer<RegisterCorporateProfileRequest> _registerCorporateProfileRequestFixture;

    public RegisterCorporateProfileRequestTestData()
    {
        _registerCorporateProfileRequestFixture = Fixture.Build<RegisterCorporateProfileRequest>();
    }

    public RegisterCorporateProfileRequest Create()
    {
        return _registerCorporateProfileRequestFixture.Create();
    }

    public RegisterCorporateProfileRequestTestData WithNationalId(string nationalId)
    {
        _registerCorporateProfileRequestFixture =
            _registerCorporateProfileRequestFixture.With(request => request.NationalId, nationalId);

        return this;
    }

    public RegisterCorporateProfileRequestTestData WithName(int length)
    {
        var name = new string('X', length);
        _registerCorporateProfileRequestFixture =
            _registerCorporateProfileRequestFixture.With(request => request.Name, name);

        return this;
    }

    public RegisterCorporateProfileRequestTestData WithEconomicId(string economicId)
    {
        _registerCorporateProfileRequestFixture =
            _registerCorporateProfileRequestFixture.With(request => request.EconomicId, economicId);

        return this;
    }

    public RegisterCorporateProfileRequestTestData WithEconomicId(int length)
    {
        var economicId = new string('X', length);
        _registerCorporateProfileRequestFixture =
            _registerCorporateProfileRequestFixture.With(request => request.EconomicId, economicId);

        return this;
    }
}

