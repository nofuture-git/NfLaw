# NoFuture.Law

### Summary

A collection of code to represent legal doctrine as a form of personal notes.

---
### Overview

---
This code is basically how I independently studied Law.  I got my law textbooks from https://www.cali.org/.  Along with some purchased ones off Amazon.com.  

The project is basically organized around typical first-year law studies (e.g. Contract, Tort, Criminal, etc.).  The real fun was that each unit-tests will typically represent an actual case.  So the doctrine becomes the source and the tests are the actual cases! 

Overall, the code operates as a giant outline, like one would find with a law student only in the form of code.  

There is a lot to unpack here because the study of Law is a vast and ancient discipline. I found the best tool to capture legal doctrine in code was the use of Predicate Logic.  Having doctrine as simple Boolean Logic was impossible because every case is different and unique.  Using Predicate Logic forced the unit-test writer to specify the facts and the results while still maintaining the overall logic of the doctrine.

Furthermore, I made extensive use of dotnet interfaces since they allow for something like multiple-inheritance. This was very important when it comes to the classifying the parties of a case.  I used a very general concrete type of `LegalPerson` which has a lot of utilities for adding titles and labels.  Furthermore, at the `~/source/Core/US/Persons` is all the "kinds" of parties I came across while studying (e.g. `IEmployee`, `IPlaintiff`, `ITortfeasor`, etc.).  These are all dotnet interfaces and therefore any concrete extension of `LegalPerson` could also extend one or more of these interfaces.

Lastly, the main operation is the `IsValid` method on the `ILegalConcept` interface.  So typically, how this worked for me was I would read a case, define the general concepts as an extension of the abstract `LegalConcept` type (which implements the `ILegalConcept` interface).  Likewise define the parties as extension of the `ILegalPerson` interface, and then draw the doctrine as predicate logic within the implementation of the given `LegalConcept`'s `IsValid` method.  Therefore, the general operation is a cascade of calls to `IsValid` of legal concepts upon which other legal concepts are constructed.  And finally, `AddReasonEntry` is a kind of ledger that explains what happened inside the call to `IsValid`.  This helps explain why a unit test may run red when its unexpected.  The unit test runner will typically print these reasons to the console. 

### A Working Example

So here is a working example starting at the case and working down into the legal concepts. Starting at `~/tests/CriminalTests/ActusReusTests/RobinsonvCaliforniaTests.cs` is the case *370 U.S. 660 (1962) ROBINSON  v. CALIFORNIA. No. 554. Supreme Court of United States.  Argued April 17, 1962. Decided June 25, 1962.*  This was a landmark case that struck down a California law that made it illegal to be addicted to narcotics.  This is part of Criminal Law and concerns the key concept of criminal doctrine called _Actus Reus_ which is Latin for "guilty act" and is synonymous with "criminal act".  So, in the above mentioned file we see the defendant as an extension of `LegalPerson` concrete type and also an implementation of the `IDefendant` interface.  The single unit test sets its `testSubject` as an instance of the `ActusReus` concrete type.  In order for the instance of `ActusReus`'s `IsValid` to return true - both predicates of `IsVoluntary` and `IsAction` would have to return true.  The ruling of the court was that the action portion of actus reus in this case was false because being an addict to narcotics is a "status or condition" - not an act.  In this unit test the latter predicate returns false.  Furthermore, when you run this unit test the console will print `the defendant ROBINSON, IsAction is false` which is the rationale behind why the `IsValid` returned false.  What a developer would call, "logging".


### Disclaimer

This code is, **in no way**, to be used for actual legal advice.  I am not an attorney.  I wrote this for fun and a love of studying law.

### History
This project was moved from my other repo, `31g` using the python tool `git-filter-repo` which allows for preserving the commit history of one repo into another.
