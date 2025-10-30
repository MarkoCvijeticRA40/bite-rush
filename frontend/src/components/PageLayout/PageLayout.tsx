import React from "react";
import { NavigationBar } from "../NavigationBar/NavigationBar";

interface PageLayoutProps {
    children: React.ReactNode;
}

/**
 * Component to wrap the pages with navigation
 */
export const PageLayout: React.FC<PageLayoutProps> = ({ children }) => {
    return (
        <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
            <NavigationBar />
            <main style={{ flex: 1, padding: '2rem' }}>
                {children}
            </main>
        </div>
    );
};