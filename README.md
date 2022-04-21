# Tip Tracker
Desktop application for tracking restaurant waitstaff gratuities.

## Background
This is a .NET Windows Forms application written in VB.NET.  The application was originally developed in 2008/2009 and was written to support the business process for the [Doherty Hotel](https://www.dohertyhotel.com).

## Current State
The application's code is very obviously in the style of a brand new developer just learning how to program, and it features copy/paste inheritance, Hungarian notation, and a whole lot of duplication.

The application is currently being [refactored](https://github.com/baile1mj/tip-tracker-legacy/tree/refactoring) in preparation for updating it to be generalized for use in any restaurant.  The ultimate goal is to have a clean design that decouples the UI from the business logic to allow for ease of maintenance and ease in porting the UI from one technology to another (e.g. Winforms to WPF, MAUI (?), or even ASP.NET).

### Roadmap
- Initial code cleanup
    - Review current state
    - Remove dead code 
    - Remove duplicate code
- Refactoring
    - Segment code (separation of concerns)
        - Isolate ADO.NET DataSet persistence behind anti-corruption layer
        - Introduce business objects
        - Introduce service layer ("rough draft" architecture)
        - Replace GDI+ reports with SSRS reports (may be later replaced with FastReports)
        - Replace file storage mechanism/obfuscation
        - Vision of "final" archicture should start to emerge
    - Refine business model
        - Reduce complexity from statefulness
        - Generalize as much as possible
        - Re-evaluate decisions from prior refactoring stage
    - Evaluate feasibility of UI update
        - Possibly change architecture only (e.g. MVP)
        - Possibly update underlying technology
- Enhancements
    - Broad scale generalization
        - Pay periods > 2 weeks
        - Adjustable pay period start/end dates
        - Eliminate "template" items (servers, config settings)
            - "Roll forward" from prior pay period
        - Review business objects
            - "Special Function" -> "Event," "Party," etc.
            - Paid vs. claimed tips
            - Daily vs. pay period tips
    - New features
        - Tip split tracking
    