 1. **Pull Project and Navigate to the StoreApp Folder Using CMD or PowerShell**

    ```shell
    git clone [project-repo-url]
    ```

2. **Open a Command Window in the StoreApp Folder**
  

3. **Finally, Run the dotnet run or dotnet watch Command**

    In the command window within the StoreApp folder, execute the following command:
	
	Fist:
	
	```shell
	dotnet ef migrations add [any-first-init-name]
    ```

	Later:
	
    ```shell
    dotnet run
    ```
    or 
    
    ```shell
    dotnet watch
    ```

    This command will watch your project and automatically recompile it when any changes are made.

