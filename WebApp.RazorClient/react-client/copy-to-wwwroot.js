const { exec } = require("child_process");
const fs = require("fs-extra");
const path = require("path");

// Set the directories
const buildDir = path.join(__dirname, "build"); // The build directory for CRA
const wwwrootDir = path.join(__dirname, "..", "wwwroot", "react-build"); // Updated path

// Function to build the React app
const buildReactApp = () => {
    return new Promise((resolve, reject) => {
        exec("npm run build", { cwd: path.join(__dirname) }, (error, stdout, stderr) => {
            if (error) {
                console.error(`Error building React app: ${stderr}`);
                return reject(error);
            }
            console.log(stdout);
            resolve();
        });
    });
};

// Function to clean the wwwroot/react-build directory
const cleanWwwroot = async () => {
    // try {
    //     await fs.emptyDir(wwwrootDir);
    //     console.log('Cleaned wwwroot/react-build directory successfully!');
    // } catch (error) {
    //     console.error('Error cleaning wwwroot/react-build:', error);
    // }

    try {
        const files = await fs.readdir(wwwrootDir);

        // Filter out asset.json from the files to be deleted
        const filesToDelete = files.filter((file) => file !== "asset-manifest.json");

        // Delete each file/directory except asset.json
        await Promise.all(filesToDelete.map((file) => fs.remove(path.join(wwwrootDir, file))));

        console.log("Cleaned wwwroot/react-build directory successfully, excluding asset.json!");
    } catch (error) {
        console.error("Error cleaning wwwroot/react-build:", error);
    }
};

// Function to copy files to wwwroot/react-build
const copyToWwwroot = async () => {
    try {
        await cleanWwwroot(); // Clean the directory first
        await buildReactApp(); // Build the app
        await fs.copy(buildDir, wwwrootDir); // Copy the new files
        console.log("Files copied to wwwroot/react-build successfully!");
    } catch (error) {
        console.error("Error copying files:", error);
    }
};

// Execute the copy function
copyToWwwroot();
