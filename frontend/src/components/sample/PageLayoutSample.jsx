import React from "react";
import { NavigationBar } from "./NavigationBarSample";

export const PageLayout = ({ children }) => {
    return (
        <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
            <NavigationBar />
            <main style={{ flex: 1, padding: '2rem' }}>
                <div style={{ textAlign: 'center', marginBottom: '2rem' }}>
                    <h5>Welcome to the Microsoft Authentication Library For React Tutorial</h5>
                </div>
                {children}
            </main>
        </div>
    );
};