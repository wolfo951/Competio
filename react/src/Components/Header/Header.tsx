import { useMemo } from 'react';
import { Link } from 'react-router-dom';
import sessionHook from '../../services/local/GetSession';
import { Session } from '../../types/Models/Session';
import './Header.scss';

const GetHeaders = (loggedIn: boolean, session: Session | undefined) => {
    const headerItems: HeaderLink[] = [
        {
            id: 'home_page',
            content: 'Home Page',
            displayed: true,
            link: '/'
        },
        {
            id: 'login',
            content: 'Login',
            displayed: !loggedIn,
            link: '/login'
        },
        {
            id: 'register',
            content: 'Register',
            displayed: !loggedIn,
            link: '/register'
        },
        {
            id: 'tournament',
            content: 'Tournaments',
            displayed: loggedIn,
            link: '/tournaments'
        },
        {
            id: 'create_tournament',
            content: 'Create Tournament',
            displayed: loggedIn && (session?.role === 'moderator' || session?.role === 'administrator'),
            link: '/create-tournament'
        },
        {
            id: 'public_tournaments',
            content: 'Public Tournaments',
            displayed: true,
            link: '/public-tournaments'
        },
        {
            id: 'user',
            content: 'My User',
            displayed: loggedIn,
            link: '/user'
        },
        {
            id: 'logout',
            content: 'Logout',
            displayed: loggedIn,
            link: '/',
            onClick: () => sessionHook.removeSession()
        }
    ];
    return headerItems;
};

export const Header = () => {
    const userInfo = sessionHook.getSession();
    const headersContent = useMemo(() => {
        const loggedIn = userInfo !== undefined;
        const getHeaders = GetHeaders(loggedIn, userInfo);

        return getHeaders
            .filter((i) => i.displayed)
            .map((header) => (
                <Link
                    className="link"
                    to={header.link}
                    key={header.id}
                    onClick={
                        header.onClick !== undefined
                            ? () => {
                                  header.onClick!();
                                  window.location.reload();
                              }
                            : undefined
                    }
                >
                    {header.content}
                </Link>
            ));
    }, [userInfo]);
    return <div style={{ margin: '15px' }}>{headersContent}</div>;
};

type HeaderLink = {
    id: string;
    link: string;
    content: JSX.Element | string;
    displayed: boolean;
    onClick?: () => void;
};
