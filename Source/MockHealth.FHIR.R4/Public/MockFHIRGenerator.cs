/*
Copyright © 2024 JC Stevens

   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Hl7.Fhir.Model;
using Muthink.MockHealth.FHIR.R4.Internal;

namespace Muthink.MockHealth.FHIR.R4;

public class MockFHIRGenerator(int seed = 0) : MockFHIRGeneratorBase(seed)
{
    public Patient GeneratePatient()
    {
        var patient = new Patient
        {
            Address = [GenerateAddress()],
            Name = [GenerateHumanName()],
            Id = Mocker.Next(100,1000000).ToString(),
            Gender = AdministrativeGender.Male,
            BirthDate = Mocker.BirthDate(1,100).ToString("yyyy-MM-dd")
        };
        return patient;
    }

    public Observation GenerateObservation(Patient patient, int index = 1)
    {
        var observation = new Observation
        {
            Id = $"{patient.Id}.{index}",
            Status = ObservationStatus.Final,
            Code = new CodeableConcept("http://loinc.org", "8480-6", "Systolic Blood Pressure"),
            Subject = new ResourceReference(patient.Id),
            Value = new Quantity
            {
                Value = 120,
                Unit = "mmHg",
                System = "http://unitsofmeasure.org",
                Code = "mm[Hg]"
            },
            Effective = new FhirDateTime(DateTime.UtcNow.AddDays(-1))
        };
        return observation;
    }

    public Condition GenerateCondition(Patient patient, int index = 1)
    {
        var condition = new Condition
        {
            Id = $"{patient.Id}.{index}",
            ClinicalStatus = new CodeableConcept("http://terminology.hl7.org/CodeSystem/condition-clinical", "active"),
            VerificationStatus = new CodeableConcept("http://terminology.hl7.org/CodeSystem/condition-ver-status", "confirmed"),
            Code = new CodeableConcept("http://snomed.info/sct", "38341003", "Hypertension"),
            Subject = new ResourceReference(patient.Id),
            Onset = new FhirDateTime(DateTime.UtcNow.AddYears(-5))
        };
        return condition;
    }

    public Encounter GenerateEncounter(Patient patient, int index = 1)
    {
        var encounter = new Encounter
        {
            Id = $"{patient.Id}.{index}",
            Status = Encounter.EncounterStatus.Finished,
            Class = new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "AMB", "ambulatory"),
            Subject = new ResourceReference(patient.Id),
            Period = new Period
            {
                Start = DateTime.UtcNow.AddDays(-7).ToString("o"),
                End = DateTime.UtcNow.AddDays(-7).AddHours(1).ToString("o")
            }
        };
        return encounter;
    }

    /// <summary>
    /// Generate a patient bundle
    /// </summary>
    /// <returns></returns>
    public Bundle GenerateBundle()
    {
        var bundle = new Bundle();
        var patient = GeneratePatient();
        bundle.AddResourceEntry(patient, patient.Id);
        var observation = GenerateObservation(patient);
        bundle.AddResourceEntry(observation, observation.Id);
        var condition = GenerateCondition(patient);
        bundle.AddResourceEntry(condition, condition.Id);
        var encounter = GenerateEncounter(patient);
        bundle.AddResourceEntry(encounter, encounter.Id);

        return bundle;
    }
}