The PrBarFillLogic class is part of the Tools.PrBarFillLogic namespace in C#. This class is responsible for managing the logic of filling a progress bar and updating the progress bar text in a user interface.  
Here's a brief overview of its functionality:  
- The class is initialized with a RectTransform object representing the progress bar fill image, an optional TextMeshProUGUI object representing the progress bar text, and an optional TextType enum value representing the type of progress bar text (percent or value).  
- The LoadingFill method fills the progress bar over a specified time and calls an optional onComplete action when finished.  
- The UpdateBarFill method updates the progress bar fill based on the current and maximum values over a specified time and calls an optional onComplete action when finished.  
- The SetProgressText method sets the progress bar text depending on the TextType. If TextType is Percent, the text will be in percent format. If TextType is Value, the text will be in the format of the current/maximum value.
- 
This class is likely used in a game or application where visual progress bars are needed to display the progress of a task or operation to the user.

Important, the script uses DOTween, you need to install it.
