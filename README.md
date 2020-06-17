# Tim5
# How does project work?
  
  When the project is launched, a window opens with a menu with options for manipulating data. If the option for automatic data entry (in the Dumping Buffer) is selected, the data is entered automatically and the user returns to the home menu. By selecting the second option (manual entry - directly in the Historical component), you get to the second menu where the user can choose what kind of CODE his data will have. Selecting CODE requires the user to enter the geo id, and then consumption, after which the data is sent to the Historical component. By pressing “ENTER“ button allows the user to re-enter the data parameters and resend it to Historical component. Pressing “ESCAPE“ button the user returns to the home menu. If in the initial menu the user selects the third option (Read data), then a new menu opens in which he can choose which CODE he wants to read. Selecting one of the CODEs displays everything from the database with the selected code. Pressing “ESCAPE” button the user returns to the home menu. If the user selects the fourth option (Exit app), then the application closes itself.
  
# Components

#1.	Writer component – used for writing new data in two ways:
  •	Direct to Historical component (SendToHistorical) and
  •	Every 2 seconds writing to Dumping Buffer (SendToDumpingBuffer).
Data sent by the Writer component are Code, Value.

#2.	Reader component – used for reading data from database. He is communicating with Historical component (GetChangesForInterval), which reads data from the database.

#3.	Dumping Buffer component – used to temporarily store data before it forward to Historical component (WriteToDumpingBuffer). 

#4.	Historical component – used to work with data (ManualWriteToHistory, ReadOneLDFromDB, ReadFromDumpingBuffer). Dumping Buffer component and Writer component are giving data for Historical component so it can manipulate with them.

#5.	Logger component – used to record all activities in other components.

# What we did in project

In this project we used components: Historical component, Writer component, Reader component, Dumping Buffer component, Logger component and Database. Each component was made in separate branches and they have their own function. We used Microsoft SQL Server Management Studio 18 for communication with SQL database.

# Diagram

Diagram.png

