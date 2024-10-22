import React from "react";
import ReactDOM from "react-dom/client";
import App from "./pages/App";
import reportWebVitals from "./reportWebVitals";

import "./index.css";

const componentMap = {
    AppPage: App,
};

// const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);
// root.render(
//     <React.StrictMode>
//         <App />
//     </React.StrictMode>
// );

window.renderReactComponent = (componentName) => {
    const Component = componentMap[componentName];
    if (Component) {
        // ReactDOM.render(
        //     <React.StrictMode>
        //         <Component />
        //     </React.StrictMode>,
        //     document.getElementById("react-root")
        // );

        const rootElement = document.getElementById("react-root");
        const root = ReactDOM.createRoot(rootElement);
        root.render(
            <React.StrictMode>
                <Component />
            </React.StrictMode>
        );
    }
};

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
