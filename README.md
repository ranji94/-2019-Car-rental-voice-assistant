# 2020 Car rental voice assistant
Project of application used to rent a cars via intelligent voice assistant

### Presentational layer
Application provides GUI based on WPF pages and inspired by **wizard UX design pattern**. Specific pages goes sequently and contains inputs which are filled manually or by using speech asynchronously. Last page presents summary with informations about our car rent order. Additionally application supports avoidance of wizard process by speaking complete expression contains all necessary data and jumps from first to last step directly.

### Data processing
Entered data is filled to form object and validated. If all necessary data were collected, complete order is saved to **MS SQL database**.

### Speech processing
Application using **Microsoft Speech Platform** library to listening to expected expressions during the whole process asynchronously. Expected expressions were mapped using Grammar class and are served conditionally by application to process entered data.

### Installing needed libraries
1. Install Microsoft Speech Platform 11 **Runtime**
2. Install Microsoft Speech Platform 11 **SDK**
3. Install MSSpeech_TTS_<LANGUAGE> nad MSSpeech_SR_<Language>
