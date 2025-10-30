import React from "react";

/**
 * Renders the navbar component with just the brand name
 */
export const NavigationBar: React.FC = () => {
    return (
        <nav style={{ 
            display: 'flex', 
            justifyContent: 'space-between', 
            alignItems: 'center', 
            padding: '1rem 2rem', 
            backgroundColor: '#2c3e50', 
            color: 'white',
            boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
        }}>
            <div>
                <h1 style={{ margin: 0, fontSize: '1.8rem', color: '#ecf0f1' }}>Bite Rush</h1>
            </div>
        </nav>
    );
};