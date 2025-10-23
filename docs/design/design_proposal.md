---
title: Team Design Document
---

**Course:** CSSD1161 --- Communication & Teamwork in Software
Development\
**Team: JAKS**

**Repo: https://github.com/KSFn1/PassVault.git**

**Term:** Fall 2025\
**Version: 1.0**

**Last Updated:** 2025/10/22

# 0) Revision History

  -----------------------------------------------------------------------
  Version           Date              Author            Summary of
                                                        Changes
  ----------------- ----------------- ----------------- -----------------
  0.1               2025-10-13        JAKS Team         Initial draft

  -----------------------------------------------------------------------

# 1) Project Overview

1.1 Problem Statement\
\_What problem are you solving and for whom? (3--5 sentences)\_

We are trying to help people who are constantly online and use many
accounts to be able to keep track of their usernames and passwords
reliably. We want to ensure that the information is stored securely with
advanced encryption methods. Lastly, we want our users to be able to
access their stored information safely and fast, therefore, we will
require users to login before being able to access their stored
information and once logged in, there will be a search bar where they
can easily search for what they're looking for.

1.2 Users & Context\
- Primary users: Anyone with digital presence of any age\
- User needs: Being able to access data securely, saving data securely,
and being able to search for information easily.\
- Context & constraints: Hard to manage different accounts on multiple
devices, which makes passwords harder to remember.

1.3 Value Proposition\
Our project provides a secure and efficient way for users to manage
their digital credentials across multiple platforms. Unline traditional
password storage methods that can be easily compromised or disorganized,
our solution uses advanced encryption methods to ensure that user data
remains private and protected. With a simple, user-friendly interface,
individuals can quickly log in and access their stored information
through an integrated search feature, eliminating the frustration of
forgotten passwords. By combining security, spoeed, and accessibility,
our system gives users the power to take control of their digital
presence with confidence and peace of mind.

1.4 Goals & Non‑Goals

> **Going to implement:**

-   Develop a secure password management system, designed for Windows,
    that allows users to store and retrieve their login credentials
    safely.

-   Implement user authentication to make sure only authorized users can
    access stored data.

-   Integrate advanced encryption techniques to protect sensitive
    information

-   Provide an efficient search feature to quickly find saved usernames
    and passwords.

-   Design a simple, intuitive user interface for easy navigation and
    usability.

**Not going to implement:**

-   Implementing password sharing or team based account management
    feature

-   Integrating biometric authentication like fingerprint and face ID

-   Building browser extensions or mobile app versions

-   Implementing multi device synchronization or cloud backup generation

-   Automatically saving new information or giving users a pop up option
    of their username and password when logging into a platform

# 2) Requirements

2.1 User Stories (MoSCoW)\
List at least 6 with ID and priority.

  --------------------------------------------------------------------------
  ID             As a...        I want...      So that...     Priority
                                                              (M/S/C/W)
  -------------- -------------- -------------- -------------- --------------
  1\)            user           Password       Users access   M
                                storing        them through   
                                               Passvault      

  2\)            user           Password       Users can      M
                                generation     generate       
                                               passwords      
                                               without having 

  3\)            user           Login page     Users can have M
                                               another layer  
                                               of protection  
                                               to my stored   
                                               passwords      

  4\)            user           Simple UI      The app is     S
                                               more           
                                               accessible     

  5\)            user           Encryption     The passwords  S
                                               are more       
                                               secure         

  6\)            developer      Easy to read   Makes things   C
                                code           easier for     
                                               future updates 
                                               and bug fixes  
  --------------------------------------------------------------------------

2.2 Acceptance Criteria (Given/When/Then)\
Trace to user stories.

1\) Given when I am logged into Passvault, enter my credentials on the
stored passwords page and click save, then the app will store the
correct credentials

2\) Given when I am logged into Passvault and click "generate password"
on the password generation page, then it will return a password with
unique letters, numbers and symbols.

3\) Given when I enter the correct credentials on Passvault login page,
then the app will automatically direct me to the other pages.

4\) Given when I want to navigate through Passvault, then the pages
should have clear information and labels

5)Given when Passvault's local data files are accessed by an
unauthorised third party, then the stored information will appear as
ciphertext and therefore, unreadable.

6\) Given when a developer wants to update a certain part of the app and
views the code, then he should be able to see clear labels in the chunks
of code.

2.3 Assumptions & Constraints\
Tech limits, time, skills, devices, data, privacy.

Tech limits: no cloud features

Devices: PC only

Time: 3--4-month development window

Skills: Each team member has their own expertise to put everything
together: developing, testing, UI design, etc.

Data/privacy: Since the app is local, unauthorized individuals without
direct access to your computer will not be able to access your
passwords.

# 3) Figma Design

Provide view-only Figma links and 2--3 exported screenshots per key
screen. [(JS)]{.underline}

3.1 Figma Links\
- Design file: https://www.figma.com/file/...\
- Prototype: https://www.figma.com/proto/...\
- Design System: https://www.figma.com/file/...

3.2 Screen Inventory

  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  Screen ID      Name           Purpose        Status         Link
  -------------- -------------- -------------- -------------- --------------------------------------------------------------------------------------------------------------------------
  S1             Sign in/Login  Takes in the   Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                 page           username and                  
                                password of                   
                                the user to                   
                                log them into                 
                                the platform.                 
                                It also gives                 
                                them the                      
                                options to                    
                                register an                   
                                account or                    
                                resert their                  
                                passwod                       

  S2             Register page  Takes in the   Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                                email,                        
                                username, and                 
                                password of                   
                                the user and                  
                                creates an                    
                                account for                   
                                them.                         

  S3             Reset          Takes in the   Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                 Password: Send email of the                  
                 email          user and sends                
                                a verification                
                                code                          

  S4             Reset          Takes the      Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                 password:      verification                  
                 Verification   code and                      
                 code           verifies the                  
                                identity of                   
                                the user                      

  S5             Reset Password Takes in the   Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                                new password                  
                                and confirms                  
                                it                            

  S6             Dashboard      Displays the   Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                                saved                         
                                usernames and                 
                                passwords in                  
                                an orginzied                  
                                way. Gives the                
                                user to                       
                                search, add,                  
                                modify, or                    
                                delete                        
                                existing                      
                                information                   

  S7             Contact Us     A form that    Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                                takes the                     
                                user's name                   
                                and email and                 
                                message and                   
                                sends it to an                
                                email address                 

  S8             About Us       Displays       Done           https://www.figma.com/design/psNsHmTIXWHwQsPaF3EgrI/PassVault-Desktop-App-Prototype?node-id=2-8&p=f&t=HcOvw5WsVNPWZ5bo-0
                                information                   
                                about us                      

                                                              
  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

3.3 User Flows\
Attach Figma flow(s) and list success criteria for each flow.

3.4 Accessibility in Design\
Color contrast, focus order, labels/errors.

Paste 2--3 key Figma screenshots below (insert images).

# 4) Information Architecture & Data

4.1 Sitemap / Navigation\
Draw or link a simple diagram (can be Figma). [(JS)]{.underline}

Login Page
 ├──→ Register Page
 ├──→ Email (Forgot Password)
 │      └──→ Verification
 │             └──→ Reset Password
 └──→ Main Page
         ├──→ Contact Us
         └──→ About Us

4.2 Content / Data Model

  ------------------------------------------------------------------------------
  Entity                  Key Fields                     Notes
  ----------------------- ------------------------------ -----------------------
  User                    UserID, username, email,       Stoores each user's
                          password_hash, created_at      account information.
                                                         Passwords are stored as
                                                         hashed values, not
                                                         plain text. Each user
                                                         can only access their
                                                         own stored credentials

  Credential              Credential_id, user_id,        Stores the login
                          platform_name,                 credentials
                          platform_username,             (username+encrypted
                          platform_password_encrypted,   password) for each
                          created_at, updated_at         platform or website.
                                                         Linked to the USer
                                                         entity through userID.
                                                         All passwords are
                                                         encrypted locally
                                                         before being stored

  Generated Password      password_id, user_id,          Represents passwords
                          generated_password,            generated through the
                          generated_at, strength_level   in-app password
                                                         generator. Used
                                                         temporarily or saved by
                                                         user choice

  Recovery Request        request_id, user_id, email,    Used for the Forgot
                          verification_code,             Password feature.
                          expiration_time                Stores verification
                                                         codes temporarily when
                                                         a user requests a
                                                         password to reset.
                                                         Codes expire after a
                                                         set time.

  Contact Manage          Message_id, name, email,       Captures messages from
                          message_text, submitted_at     the Contact Us form.
                                                         Allows users to send
                                                         feedback or report
                                                         issues.
  ------------------------------------------------------------------------------

# 5) UI Specifications

5.1 Layouts per Screen\
Specify layout, spacing, breakpoints, and annotations.

-   The login, register, and forgot password page will be a mini square
    window with dimensions of around 600px X 600px. The Email sending
    page and the verification code page will be of smaller size, around
    400px X 400px.

-   The main dashboard, contact us, and about us will be a big window
    with dimensions of 1920px X 1080px

-   The spacing will depend on the specific elements and pages, but
    overall will remain consistent and organized on each page

Layout:

Login screen:

-   The header of the page will be dark pink with the company's logo in
    the middle of it.

-   In the center, there will be a username and a password field
    followed by a big "Login" button.

-   Below it, there will be a "forgot password" button for those who
    have forgotten their password

-   Below that, there will be an option to create an account for those
    who don't have an account yet.

-   In the center of the footer, there is a Copyright statement and, in
    the bottom, right of the page, the logo of the company will be
    placed.

Register page:

-   The header and footer remain the same as the Login page

-   Similar to the login page, there will be text fields for user input
    centered in the page asking for email, username, password, and
    password confirmation.

-   A big "Register" button is placed under the last text field as well

Forgot password:

-   The header and footer remain the same as every other page

Email:

-   A text field that takes the user's email with a button at the bottom
    that sends verifies the email.

> Verification Code:

-   A text field that takes the verification code with a button at the
    bottom that sends verifies the email.

> Reset password:

-   Two text fields that take the new password and confirm the new
    password

-   A cancel and a submit button are at the bottom of the page, and they
    both direct the user to the login page.

Main Dashboard:

-   The header and footer remain the same as every other page with the
    only difference that that header will now have a big "Home" text in
    the middle of it as well as a nav bar that allows the user to access
    the Contact Us and About Us pages. The logo has now been moved to
    the left side of the header rather than the center. Clicking the
    logo will take you to the home page.

-   The page is set up with rows and columns, where the rows are the
    information for each platform and columns are the same for every row

-   The first row will have a "platform", "username", "password", and an
    add button that allows the user to add new information.

-   Every row after that the platform's name, username, password, with a
    "modify, and "delete buttons" that allows the user to either edit
    the information for that platform or completely delete it.

-   There will be a logout button at the top left of the page that logs
    the user out of their account and closes the app.

Contact Us & About Us:

-   The header and footer remain the same as every other page with the
    only difference that that header will now have a big "Contact
    Us"/"About Us" text in the middle of it as well as a nav bar that
    allows the user to access the Contact Us and About Us pages. The
    logo has now been moved to the left side of the header rather than
    the center. Clicking the logo will take you to the home page.

-   The Contact Us page will have a form that takes the user's name,
    email address that they want to be reached at, and their concern
    with a submit button.

-   The about us page will have a description of who we are and what our
    objective is.

5.2 Style Guide\
- Colors (hex + roles)

-   670A7E for header and positive action buttons (login, register,
    submit)

-   20A6C4 for negative action buttons (cancel)

-   000000 for texts and footer

-   FFFFFF for background color, header titles (page name, nav titles),
    and footer texts

-   FF0000 for submit button on the contact us page, the delete button
    on the dashboard, and the logout button.

-   1A6D13 for the "Add" and modify button on the dashboard.

-   EEEEEE/D9D9D9 for text background fields

\- Typography (headings/body)

-   All texts are in the "Inter" font or similar, depending on what the
    IDE supports

-   Headings are bolded and in upper case, 40px

-   Subheadings are not bolded and written normally, 20px

-   Normal texts are written normally, 11px

\- Iconography

We will not use icons in our solution

\- Components & states (default/hover/focus/disabled/error)

-   The default buttons are as outlined above in terms of color.

-   Any button that is hovered over will turn 2 shades darker

-   Disabled buttons will have their texts turned white and are not
    hover able

-   Our product does not require error or focus buttons.

# 6) Interaction Design

**States & behaviors: loading, empty, success, error.**

> Loading:

-   When saving or retrieving passwords, a spinner or progress indicator
    is shown.

> Empty:

-   If the user hasn't stored any passwords yet, an empty state will be
    shown displaying text that reads "No passwords saved yet" or
    something along those lines. It will also display a button which you
    can use to add one.

Success:

-   After adding, editing, or deleting a password, a text will be shown
    that confirms that action.

> Error:

-   If a save fails or the user inputs something invalid, an error
    message will be displayed, with helpful information about the error.
    i.e. if the user inputs something invalid, they will be told to
    enter the right input.

**Keyboard interactions and focus management.**

-   Tab navigation is supported across all fields and buttons.

-   When a page is opened, the first place that has input gets focus.

-   After an action is performed (like saving), focus returns somewhere
    logical.

**Microcopy: errors, empty states, tooltips, button labels.**

Errors:

-   Will be short and direct. I.e. "Passwords cannot be empty".

Empty States:

-   Will be short and direct. I.e. "Passwords cannot be empty".\
    \
    Tooltips:

-   Used for icons or important actions. E.g., hovering over the delete
    icon will display\
    "Delete this password".\
    \
    Button Labels:

-   Clear action verbs like Add, Save, Edit, Delete, Cancel.

# 7) System Overview (Lightweight)

Architecture, services/APIs, simple diagram.\
Table of external services if any.

-   We will use Google API to send emails

Architecture/components:

1)  Login page

2)  Password recovery tool

3)  Password list with modifier settings

4)  Password generator

5)  Encryption tool

# 8) GitHub Workflow (Course Conventions)

Repository URL, default branch, branching strategy, issues/milestones,
PR checklist.

**Repository URL:**

<https://github.com/JS-457/JAKS-CSSD-1661-Project.git>

**Default Branch:**

Master

**Branching Strategy:**

Master: always clean and stable

Test: where new features are tested and combined

Feature branches: one branch for each feature

**Issues & Milestones:**

Issues help track bugs, improvements, and future tasks.

Milestones merge issues into goals for a project (v1.0)

Example issues and milestones created on GitHub.

**PR Checklist (Pull Requests)**

\[ \] Code runs locally with no errors

\[ \] Comments and variable names are clear

\[ \] README and documentation updated

\[ \] No console errors or print statements

\[ \] Code has been reviewed by a reviewer

# 9) Testing Plan

Usability checks with 3 participants (short task script, success
criteria).\
Functional checks (happy/edge paths) with traceability to stories.

Task 1: Create Passvault credentials for the login page.

Success Criteria: be able to login within 90 seconds successfully

Task 2: enter 3 different email/password combinations and manage to
store them under a given name.

Success Criteria: The app registers these credentials and when logged
out and back in, the passwords are still stored inside.

Task 3: Ask the app to generate a password about 15 times and compare
each generated password for uniqueness

Success Criteria: all generated passwords are unique

# 10) Accessibility & Inclusion

Keyboard-only navigation, alt text, labels, error recovery help,
inclusive language.

We will have clear labels with simple dictionary and added descriptions

# 11) Risks & Mitigations

  -----------------------------------------------------------------------
  Risk              Impact            Likelihood        Mitigation
  ----------------- ----------------- ----------------- -----------------
  Passvault         High              High              Password recovery
  credentials                                           feature
  forgotten                                             

  Data Breach       High              Very low          Encryption, no
                                                        cloud backing

                                                        
  -----------------------------------------------------------------------

# 12) Milestones & Schedule

  -----------------------------------------------------------------------
  Milestone               Date                    Deliverable
  ----------------------- ----------------------- -----------------------
  Design Freeze           Oct 19, 2025            Final Figma screes,
                                                  flows and interaction
                                                  states (loading, empty,
                                                  success, error)

  MVP Build               Nov 28 , 2025           Core app features
                                                  finalized (Adding,
                                                  viewing, deleting and
                                                  storing of usernames
                                                  and passwords)

  Demo                    Nov 28 , 2025           Fully working demo with
                                                  good User Interface,
                                                  error handling and
                                                  navigation
  -----------------------------------------------------------------------

# 13) Submission Checklist

\[ \] Figma links (design + prototype)\
\[ \] Screenshots per key screen\
\[ \] User stories + acceptance criteria\
\[ \] Screen inventory & flows\
\[ \] Style guide & interaction states\
\[ \] GitHub repo URL and PR/Issue links\
\[ \] Usability + functional checks\
\[ \] Accessibility notes\
\[ \] Risks & timeline
