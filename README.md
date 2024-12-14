# Evaluation Space

## **CI Pipeline**

[![Node.js CI](https://github.com/inginerie-software-22-23/proiect-inginerie-software-alt255/actions/workflows/node.js.yml/badge.svg?branch=main)](https://github.com/inginerie-software-22-23/proiect-inginerie-software-alt255/actions/workflows/node.js.yml)

[![.NET](https://github.com/inginerie-software-22-23/proiect-inginerie-software-alt255/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/inginerie-software-22-23/proiect-inginerie-software-alt255/actions/workflows/dotnet.yml)

## **Problem Statement**

Nowadays, the teachers do not always have the possibility to digitize the whole system of evaluation. In addition to this, they would want to share tests and to have centralized all the statistics regarding the results.  

Thus, we came up with the idea of developing a web application that offers all these features in one place. The professors now can evaluate their students in a very fast, efficient and organized way without using any paper. Moreover, both evaluation methods and their results can be stored for a long period of time without the possibility of being lost.
<br></br>
## **Functional Decomposition**


<table>
  <tr>
   <td><strong>Perspective</strong>
   </td>
   <td><strong>Module</strong>
   </td>
   <td><strong>Description</strong>
   </td>
   <td><strong>Functionality</strong>
   </td>
   <td><strong>Implementation Priority</strong>
   </td>
  </tr>
  <tr>
   <td rowspan="18" >Teacher
   </td>
   <td rowspan="4" >Teacher account
   </td>
   <td rowspan="4" >First focus of the project is for the teachers to create accounts on the platform, so that a solid database can be built. By creating an account, a teacher can create quizzes and get in touch with students
   </td>
   <td>Create an account using a valid email address
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td>Edit Profile
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td>View Profile
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td>Delete profile
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td rowspan="3" >Classroom
   </td>
   <td rowspan="3" >The second part of the application focuses on assigning the correct students to the teachers. Teachers can select classrooms and assign students to the classrooms in order to generate/send quizzes
   </td>
   <td>Select classroom(s) at register
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Edit classroom(add/delete student)
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>View students
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td rowspan="5" >Quiz
   </td>
   <td rowspan="5" >After selecting the classrooms the teacher must have the ability to create a quiz/test that he can send to the students. Attributes of the quiz: start time, score, number of questions
   </td>
   <td>Create quiz
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Edit quiz (assign question to quiz)
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Delete quiz
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Set quiz start time
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Send quiz
   </td>
   <td>Sprint 3
   </td>
  </tr>
  <tr>
   <td rowspan="3" >Question
   </td>
   <td rowspan="3" >The teacher has the ability to create questions that are included in the quiz. The teacher can choose from a multitude of question types, such as: fill in the gaps, multiple choice, etc.
   </td>
   <td>Create question
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Edit question
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Delete question
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td rowspan="3" >Results Section
   </td>
   <td rowspan="3" >After the students take the quiz, the teacher can see a detailed overview of the results, with statistic for each class/student
   </td>
   <td>View statistic for classroom
   </td>
   <td>Sprint 3
   </td>
  </tr>
  <tr>
   <td>View statistic for student
   </td>
   <td>Sprint 3
   </td>
  </tr>
  <tr>
   <td>View statistic for all categories
   </td>
   <td>Sprint 3
   </td>
  </tr>
  <tr>
   <td rowspan="9" >Student
   </td>
   <td rowspan="6" >Student account
   </td>
   <td rowspan="6" >On the other end of the application, the first feature that needs to be implemented is student accounts. A student can register in the application, can take a quiz, see the result of the quiz, view his classroom and teachers he's assigned to
   </td>
   <td>Create an account using a valid email address
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td>Edit Profile
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td>View Profile
   </td>
   <td>Sprint 1
   </td>
  </tr>
  <tr>
   <td rowspan="3" >Delete Profile
   </td>
   <td rowspan="3" >Sprint 1
   </td>
  </tr>
  <tr>
  </tr>
  <tr>
  </tr>
  <tr>
   <td>Quiz
   </td>
   <td>The student receives quizzes and has the ability to take them in the application
   </td>
   <td>Take quiz
   </td>
   <td>Sprint 2
   </td>
  </tr>
  <tr>
   <td>Classroom
   </td>
   <td>The student is added to classroom by the teacher/select classroom at register. The student only has the ability to view the classroom he has been assigned to
   </td>
   <td>View classroom
   </td>
   <td>Sprint 3
   </td>
  </tr>
  <tr>
   <td>Results Section
   </td>
   <td>The student can see a detailed overview of the quizzes that he has taken
   </td>
   <td>View statistics
   </td>
   <td>Sprint 3
   </td>
  </tr>
</table>

<br></br>
## **Non-Functional Requirements**
* **Scalability** : More students can take a test at the same time.
* **Integrity** : A student cannot modify his/her/their results.
* **Security** : Account locked after 3 failed attempts.
* **Usability** : Our interface is easy to use for both teachers and students.
* **Portability** : Our app can be used on any device that supports a web browser.
* **Privacy** : The information of one student can be seen by his/her/them colleagues only if he/she/them decides to. The teacher is the only person that can see all the results.
<br></br>

## **Activity Diagram**

Quiz Activity             |  Login Activity
:-------------------------:|:-------------------------:
![diagrama1](https://user-images.githubusercontent.com/45512830/204339983-18ffee4e-61cf-47e0-b03b-406ae181efb3.png)  |  ![diagrama2](https://user-images.githubusercontent.com/45512830/204347002-22457455-8923-4f7d-86c7-903fa9877308.jpeg)

<br></br>
## **Prioritized product backlog**
The **product backlog and the user stories** can be found [here](https://github.com/orgs/inginerie-software-22-23/projects/41)
<br></br>
## **Project charter**

<table>
  <tr>
   <td>Project Title
   </td>
   <td colspan="4" ><strong>Evaluation Space</strong>
   </td>
  </tr>
  <tr>
   <td>Project Description
   </td>
   <td colspan="4" >Web platform that teachers can use to evaluate students by creating and sending online tests to students.The application also offers statistics about the test results that can be viewed by students and teachers.
   </td>
  </tr>
  <tr>
   <td>Project Objectives
   </td>
   <td colspan="4" >
<ul>

<li><strong>Enhance the quality of learning and teaching</strong> by providing a user friendly environment for organizing school related activities

<li><strong>Improve the efficiency and effectiveness</strong> of class assignment by reducing the usual paperwork needed for taking tests

<li><strong>Offer personalized evaluation solutions</strong> for teachers by allowing them to create custom quizzes for their classes

<li><strong>Give helpful statistics of the test results</strong> for both students and teachers that can improve the academic activity by better understanding the outcomes of a test
</li>
</ul>
   </td>
  </tr>
  <tr>
   <td>Project Scope
   </td>
   <td colspan="4" >Deliver an application that students and teachers can login to, in order for teachers to assess their classrooms by means of online tests.
   </td>
  </tr>
  <tr>
   <td rowspan="3" >Team
   </td>
   <td>Project Manager
   </td>
   <td colspan="3" >Pirlogea Luciana-Elena
   </td>
  </tr>
  <tr>
   <td>Development Team
   </td>
   <td colspan="3" ><strong>Backend</strong>: Lazaroiu Teodora, Cherim Erol, Pirlogea Luciana-Elena, Gutu Stefania-Alexandra
<p>
<strong>Frontend</strong>: Dobre Adriana Lia, Pirlogea Luciana-Elena
   </td>
  </tr>
  <tr>
   <td>Quality Assurance Team
   </td>
   <td colspan="3" >Erol Cherim, Lazaroiu Teodora
   </td>
  </tr>
  <tr>
   <td rowspan="5" >Responsibilities
   </td>
   <td>Sprint
   </td>
   <td colspan="3" >Milestone Description
   </td>
  </tr>
  <tr>
   <td>Sprint 1
   </td>
   <td colspan="3" >Register and login functionality for all entities and basic frontend.
   </td>
  </tr>
  <tr>
   <td>Sprint 2
   </td>
   <td colspan="3" >Quiz and question creation
   </td>
  </tr>
  <tr>
   <td>Sprint 3
   </td>
   <td colspan="3" >Implement quiz sending to classrooms and statistics
   </td>
  </tr>
  <tr>
   <td>Sprint 4
   </td>
   <td colspan="3" >Application testing. Demo
   </td>
  </tr>
  <tr>
   <td>Stakeholders
   </td>
   <td colspan="4" >Universities, schools, students, teachers and development team
   </td>
  </tr>
</table>

### **Budget**

**The statistics from the following budget can be found [here](https://docs.google.com/spreadsheets/d/1OAqrUKGY4IOGtzUMleqmvRIRwXjmHsiLWSRRCNht6Oo/edit#gid=1115838130)**

<img width="387" alt="budget" src="https://user-images.githubusercontent.com/45512830/216039108-22e7d17c-76e6-47cf-a62d-d674b5d8d717.PNG">

<br></br>

## **Roadmap**

### **A more detailed roadmap can be found [here](https://docs.google.com/spreadsheets/d/1OAqrUKGY4IOGtzUMleqmvRIRwXjmHsiLWSRRCNht6Oo/edit#gid=1115838130)**

![EvaluationSpace'sROADMAP](https://user-images.githubusercontent.com/45512830/204343136-81305787-edf3-46fe-9f3f-08ee574305f2.png)
<br></br>
## **Definition of ready**



* The task is clearly summarized in a ticket from the backlog.
* The team has the knowledge and tools required to complete the task.
* An overview of the work involved in the completion of the task is clear.
* The priority of the task is clearly communicated.
* The task has been sized by the development team using a time estimation.
* The product owner is aware of the task and has accepted it.
* The team can demo/showcase the feature (if applicable).

When a task does not align with these principles, it is not considered “ready”, and it will need to be either refined further or wait for its dependencies to have been resolved.
<br></br>
## **Definition of done**



* The task must be described via test cases.
* The task should be implemented on a feature branch.
* The task should be unit tested (unit tests should only target the business logic).
* All unit tests should pass.
* The task should pass development tests (the development team will be responsible for this).
* Pull request to merge in develop.
* Task implementation should pass code reviews (a minimum of two approvals is needed before being ready for QA tests on the feature branch).
* QA acceptance tests pass on the feature branch.
* The feature branch is merged in develop, then deleted.
* QA smoke tests on develop (tests for a basic part of the functionality to ensure that it was integrated in develop).


## **Software Architecture**

![architecture drawio](https://user-images.githubusercontent.com/79518275/215559473-f6f7aa6d-7ec5-46c7-a96c-d24965be0ee2.png)

### **Repository Pattern**
We used *repositories* for accessing the database context because they provide a better maintainability and decoupling of the infrastructure. They separate the data logic layer from the rest of the layers and help in writing a clean and readable project. We also used an Object-Relational Mapper - *Entity Framework Core* to simplify the implementation of the data layer.

### **Functional Decomposition**

The role-based authentication and authorization implemented in the application offers different actions for users, based on their type of account *Student* or *Teacher*.

![Functional Diagram drawio](https://user-images.githubusercontent.com/79518275/215563062-6e6c4008-86a9-44fe-8279-92a40461f65e.png)

# QA Report

### Continuous Integration

To assure a high standard to the back-end and front-end components of the app every change made to the code is monitored using the CI pipeline which assures that the modifications made do not cause any build errors in the application. 

The CI pipeline ensures that the back-end component of the application passes the defined unit tests. 

Selenium automation tests were added to ensure that the front-end component of the application works as expected.

![image](https://user-images.githubusercontent.com/48221670/215595361-8eb8e605-dce2-43e0-85b0-e8165dde8237.png)

### Automation Testing

### Unit Tests

The Unit Tests ensure that each service works as a stand-alone component by using the **Moq** framework in order to mock the external dependencies of the services (repositories, mappers, config files). 

The application has unit tests for the following services:

- Classroom Service
- Student Service
- Teacher Service
- User Service

Technologies used: NUnit, Moq

![image](https://user-images.githubusercontent.com/48221670/215595405-c09f13ec-6a2e-4920-a368-46e868d5dc3e.png)

### UI Tests

In order to ensure that new additions to the UI project do not break existing flows, the UI tests are ran at regular intervals. The project contains one end-to-end flow with multiple tests that mocks a regular use case of the application. Only tests that meet the **acceptance** criteria of a user story were automated.

The end-to-end test contains the following tests:

- Register as Teacher
- Register as Student
- Login as Teacher
- Create Quiz from the Teacher account
- Login as Student
- Check that the quiz was created from the Student account

The tests are not included in the CI pipeline, as UI tests should not be included since running them at every build takes a considerable amout of time. Running the tests requires a separate project and pipeline, but due to the technical limitations the tests were ran on a local machine/build. 

Technologies used: Selenium, NUnit

![image](https://user-images.githubusercontent.com/48221670/215595444-64e098f4-238a-4707-a01a-024c496f6861.png)

### Acceptance and Functional testing

### Acceptance testing

Acceptance testing was done manually, in order to meet the acceptance criteria of the user stories. Tests were done on each separate branch, after having a minimum functional feature (front-end + back-end). After passing acceptance testing pull requests were merged into the main branch of the application. 

Testing procedures were not documented as test cases because the manual QA team also did the automation of the tests and there was no need for a common platform that would usually connect a manual and automation team (i.e. testrail).

### Functional testing

In order to ensure a high standard of the implementation, and to minimise potential vulnerabilities/edge cases not being covered, functional testing was also conducted. Functional testing covered cases that would not usually appear in a user’s normal flow. 

### Security concerns

The application ensures bare minimum security standards. Data privacy and integrity is covered by the technologies used, authentication and authorisation and the overall logic of the application (i.e, a student can not see another student’s grades, a teacher can only see the grades of the students assigned to him, etc.).

### Green areas:

- Basic field validation for login and register fields
- Hashing and salting of the passwords when a user registers
- Guards in frontend that prevent unauthorised access to components
- All api calls made must be authorised (except for register and login methods)

### Grey areas:

- The application does not include implemented data deletion methods that would ensure GDPR’s ******************************right to complete erasure******************************. Data can still be deleted manually by queries in the db.
- The application does not enforce SOC/SOX compliancy, there is no mechanism to store logs/metrics for any amount of time.

### Red areas:

- The application does not enforce password complexity, there is only a minimum 10 character length required when a user registers with a password.
- Login error messages are returned in frontend in an unsecure manner, a toast is displayed for each different case: ‘*****************user doesn’t exist’, ‘wrong password’***************** which would give more refined information to an attacker in case of an attempted breach


## **Sprints**

**Our deliverable for every sprint:**



* Sprint 1 - deliverable 1
* Sprint 2 - deliverable 2
* Sprint 3 - deliverable 3 (half of it)
* Hardening Sprint - deliverable 3 (half of it)

### **Sprint 1 - first sprint of development(sprint 3 - per total)**

**The link to the sprint’s backlog can be found [here](https://github.com/orgs/inginerie-software-22-23/projects/123).**

**Start time:** 28.11.2022

**End time:** 09.12.2022

**Sprint goal:** Database Setup + Authentication

![image](https://user-images.githubusercontent.com/45512830/215338774-d94ee22b-c86c-45a1-9a8a-f6191c54b5cd.png)

**Sprint Report:**

This sprint contained both backend and frontend parts. 

The tasks were fully developed in time, but the testing for ViewProfile Page UI was carried over in sprint 2’s period. 

**Retrospective Outcome:**

Went well: 
1. We estimated the tasks very well and we finished them as we expected.
2. We didn’t give up on anything.

Went bad:
1. The backend part was finished very late and we had limited time to properly connect both parts of the app.

Action Items:
1. We need to be more organized in the future.
2. The backend part needs to be finished early.

**Review Session:**

The link for the app demo can be found [here](https://youtu.be/mugT48dLbWQ).

**User stories & acceptance criteria:**

The committed stories for this sprint were:

1. As a teacher/student I want to register by creating a username and password so that the system can remember me and my activities. - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: the password length is bigger than 10

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: the email has to be unique and is assigned to one single account

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: the email has to be valid

2. As a teacher/student I want to login into my account so that I can have access to all the information the platform offers for my account type. - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: correct email

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: correct password

3. As a teacher/student I want to see my profile so that I can modify or delete it if I want to.  - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user has to be logged in


### **Sprint 2 - second sprint of development(sprint 4 - per total)**

**The link to the sprint’s backlog can be found [here](https://github.com/orgs/inginerie-software-22-23/projects/137).**

**Start time:** 12.12.2022

**End time:** 23.12.2022

**Sprint goal:** Quiz Creation + Response Submission

![image (1)](https://user-images.githubusercontent.com/45512830/215339309-08c2de9d-87ce-4eea-9f87-358502921327.png)

**Sprint Report:**

This sprint contained both backend and frontend parts. 

The tasks related to the teacher's part were fully developed and tested in time, but the student part was carried over in sprint 3 (only the frontend and the testing parts).

**Retrospective Outcome:**

Went well: 
1. We managed to overcome new challenges on the frontend part.
2. The backend part was finished early.

Went bad:
1. We didn’t expect for the frontend part to be that hard so we were late with the deadline.

Action Items:
1. We need to be more developers on the frontend side.

**Review Session:**

The link for the app demo can be found [here](https://youtu.be/-DTZdR2mPqU).

**User stories & acceptance criteria:**

The committed stories for this sprint were:

1. As a teacher I want to create quizzes so that I can evaluate my students. - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a teacher role

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: teacher can send a quiz only to his classrooms


2. As a teacher I want to see my quizzes so that I can manage them. - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a teacher role

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: teacher can edit/delete quiz only before start time


3. As a student I want to take quizzes so that I can check my knowledge level. - not delivered in this sprint.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a student role

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: student can start a quiz only after start time




### **Sprint 3 - third sprint of development(sprint 5 - per total)**

**The link to the sprint’s backlog can be found [here](https://github.com/orgs/inginerie-software-22-23/projects/181).**

**Start time:** 09.01.2023

**End time:** 20.01.2023

**Sprint goal:** Results’ Statistics and classrooms’ management 


![image (2)](https://user-images.githubusercontent.com/45512830/215339700-6a9dfe8d-fea6-4aa1-8c0a-eecaa2ff4313.png)


**Sprint Report:**

This sprint contained both backend and frontend parts. 

We worked on this sprint only for half of it. 

In this half, we managed to develop all the tasks related to the student part, including testing and the carry overs from the previous sprint.

The rest of them were carried over for the first half of the hardening sprint.

**Retrospective Outcome:**

Went well: 

1. We finally finished the most difficult part of the app which was carry over.

Went bad:

1. We didn’t start early and we worked only for half of the sprint.

Action Items:

1. We need to try to complete all the tasks in time.

**Review Session:**

This review session includes both sprint 3 and hardening sprint’s review.

The link for the app demo can be found [here](https://youtu.be/z-_1b4Vuqz4).

**User stories & acceptance criteria:**

The committed stories for this sprint were:

1. As a student I want to take quizzes so that I can check my knowledge level. - delivered (carry over)

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a student role 

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: student can start a quiz only after start time


2. As a teacher I want to see my students' results so that I can see their level of knowledge. - not delivered in this sprint

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a teacher role 


3. As a teacher I want to see my students and classrooms so that I can manage them. - not delivered in this sprint

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a teacher role 



4. As a student I want to see my teachers/classroom so that I can know who to ask for help. - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a student role 



5. As a student I want to see my quiz results so that I can improve my skills. - delivered

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a student role




### **Hardening Sprint - the last sprint of development(sprint 6 - per total)**

**The link to the sprint’s backlog can be found [here](https://github.com/orgs/inginerie-software-22-23/projects/186).**

**Start time:** 23.01.2023

**End time:** 01.02.2023

**Sprint goal:** Improvements + Tests


<img width="500" alt="hardening" src="https://user-images.githubusercontent.com/79320751/215763465-75ffe6b9-266b-4721-9793-79c4c8776e53.png">


**Sprint Report:**

This sprint was contained of frontend part and unit testing in backend. 

We worked on this sprint only for half of it. 

In this half, we managed to develop the carry overs from the previous sprint and finished most of the bugs and improvements.

The only left over was the Add Loading Spinner task which we decided to cancel.

**Retrospective Outcome:**

Went well: 

1. We finished and improved the entire app.

Went bad:

1. We didn’t approach all the tasks.

Action Items:

1. We have to estimate and structure in a better way the future sprints in the following projects.



**User stories & acceptance criteria:**

The committed stories for this sprint were(only carry overs):


1. As a teacher I want to see my students' results so that I can see their level of knowledge. - delivered (carry over)

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a teacher role 



2. As a teacher I want to see my students and classrooms so that I can manage them. - delivered (carry over)

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acceptance criteria:

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :ballot_box_with_check: user must have a teacher role




## **Sustainability and Ethics**

### Is Evaluation Space sustainable?

**Social :heavy_check_mark:**

It offers unlimited access to any educational institution and also to all the individual tutors that want to centralize their tests and statistics in one place.

**Environmental :heavy_check_mark:**

It reduces in a considerable way the use of paper and plastic.

**Economic :heavy_check_mark:**

In future implementations, the institutions would need to pay a monthly fee in order to use this app while the students and the individual tutors would use it for free.

It is not implemented yet as the app is still in trials. 

