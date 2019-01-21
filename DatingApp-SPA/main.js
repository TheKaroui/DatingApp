import { app, BrowserWindow } from 'electron';
const basepath = app.getAppPath();

let win;
/// add "." inside <base href="./"> in index.html to make electron work with it
/// npm install electron --save-dev
///create main.Js in root !!! same level as package.json
/// add main js file to package .json
// add both new commands in package.json electron and electron-build
/// npm run electron-build

function createWindow () {
  // Create the browser window.
  win = new BrowserWindow({
    width: 600, 
    height: 600,
    backgroundColor: '#ffffff',
    icon: `file://${basepath}/dist/DatingApp-SPA/assets/logo.png`
  })

  win.loadURL(`file://${basepath}/dist/DatingApp-SPA/index.html`)




  //// uncomment below to open the DevTools.
  // win.webContents.openDevTools()

  // Event when the window is closed.
  win.on('closed', function () {
    win = null
  })
}

// Create window on electron intialization
app.on('ready', createWindow)

// Quit when all windows are closed.
app.on('window-all-closed', function () {

  // On macOS specific close process
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

app.on('activate', function () {
  // macOS specific close process
  if (win === null) {
    createWindow()
  }
})