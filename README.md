# Hospital
## Task:
Create an application which automatizes hospital business processes. There are two main entities: doctors and patients. 
There are also some additional helper tables which contain link between doctors and patients, patients statuses etc.
All user input should be validated at least on server side. DAL is implemented using Entity Framework and "Repository" pattern.

The app should allow users to:

  1. see list of all doctors
  2. search docctors by name
  3. add new doctor
  4. edit existing doctors
  5. delete doctors  (with confirmation)
  6. list all patients
  7. search patients by name
  8. add new patients
  9. edit patients 
  10. assign doctors to patients 
  11. delete patients (with confirmation)
  12. use dependency injection to inject dependencies
  13. add role based authorization (doctors can do anything, patients can only view their own data)
