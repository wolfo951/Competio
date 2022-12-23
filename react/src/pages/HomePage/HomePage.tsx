import React from 'react';

export interface IHomePageProps {}

const HomePage: React.FunctionComponent<IHomePageProps> = () => {
    return (
        <div>
            <h1>Home page</h1>
            <p>This is were you would get general knowledge about the page and all the items related to it.</p>
        </div>
    );
};

export default HomePage;
