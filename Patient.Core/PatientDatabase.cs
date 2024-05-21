using Patient.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Core;
public class PatientDatabase : IPatientDatabase
{
	private PatientContext db = new();

	public Task<PrescriptionInfo> AddPrescriptionAsync(PatientInfo patient, PrescriptionInfo prescription) => throw new NotImplementedException();
	public Task<PatientInfo> CreatePatientAsync(PatientInfo patient) => throw new NotImplementedException();
	public Task<bool> DeletePatientAsync(PatientInfo patient) => throw new NotImplementedException();
	public Task<bool> DeletePrescriptionAsync(PatientInfo patient, PrescriptionInfo prescription) => throw new NotImplementedException();
	public Task<IEnumerable<PrescriptionInfo>> GetPatientPrescriptionsAsync(PatientInfo patient) => throw new NotImplementedException();
	public Task<IEnumerable<ProviderInfo>> GetPatientProvidersAsync(PatientInfo patient) => throw new NotImplementedException();
	public Task<PatientInfo> GetPatientsAsync() => throw new NotImplementedException();
	public Task<ProviderInfo> GetPrescriptionProviderAsync(PrescriptionInfo prescription) => throw new NotImplementedException();
}
