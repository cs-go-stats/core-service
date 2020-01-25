# v0.2.0
## Added
* Code from Infrastructure.DataAccess, Infrastructure.Messaging and Infrastructure.PageParse packages.
## Changed
* Ordered code in correct namespaces.

# v0.1.4
# v0.1.3
## Changed
* Actualized `data-access` package.

# v0.1.2
## Changed
* Fixed a bug in Upsert that called Update for new entity that has not been presented in database.

# v0.1.1
## Changed
* Fixed a bug when scheduler didn't start after service initialize.
* Fixed a bug for entity type registration that used wrong value for table name.
* Simplified restrictions for `AggregateRoot` constructor.

# v0.1.0
## Added
* Startup initialization and services runtime template method.
* Links extensions
* Scheduling extensions and abstractions.
* Handling and core entities abstractions.
