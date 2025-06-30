import React from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { LogOut, Plane, User, Settings, Search, Calendar } from 'lucide-react';

const Navbar = () => {
    const { user, logout } = useAuth();
    const location = useLocation();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    const getNavItems = () => {
        switch (user?.role) {
            case 'Admin':
                return [
                    { path: '/admin/dashboard', label: 'Dashboard', icon: Settings },
                    { path: '/admin/users', label: 'Users', icon: User },
                    { path: '/admin/routes', label: 'Routes', icon: Plane },
                    { path: '/admin/bookings', label: 'Bookings', icon: Calendar },
                ];
            case 'FlightOwner':
                return [
                    { path: '/flight-owner/dashboard', label: 'Dashboard', icon: Settings },
                    { path: '/flight-owner/airline', label: 'Airline', icon: Plane },
                    { path: '/flight-owner/flights', label: 'Flights', icon: Calendar },
                    { path: '/flight-owner/bookings', label: 'Bookings', icon: User },
                ];
            case 'Passenger':
                return [
                    { path: '/passenger/dashboard', label: 'Dashboard', icon: Settings },
                    { path: '/passenger/search', label: 'Search Flights', icon: Search },
                    { path: '/passenger/bookings', label: 'My Bookings', icon: Calendar },
                ];
            default:
                return [];
        }
    };

    const getRoleColor = () => {
        switch (user?.role) {
            case 'Admin':
                return 'from-blue-600 to-blue-700';
            case 'FlightOwner':
                return 'from-green-600 to-green-700';
            case 'Passenger':
                return 'from-purple-600 to-purple-700';
            default:
                return 'from-gray-600 to-gray-700';
        }
    };

    return (
        <nav className={`bg-gradient-to-r ${getRoleColor()} shadow-lg`}>
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-16">
                    <div className="flex items-center">
                        <Link to="/" className="flex items-center space-x-2 text-white font-bold text-xl">
                            <Plane className="h-8 w-8" />
                            <span>Simply Fly</span>
                        </Link>
                    </div>

                    <div className="hidden md:block">
                        <div className="ml-10 flex items-baseline space-x-4">
                            {getNavItems().map((item) => {
                                const Icon = item.icon;
                                const isActive = location.pathname === item.path;
                                return (
                                    <Link
                                        key={item.path}
                                        to={item.path}
                                        className={`flex items-center space-x-1 px-3 py-2 rounded-md text-sm font-medium transition-colors ${isActive
                                                ? 'bg-white bg-opacity-20 text-white'
                                                : 'text-white hover:bg-white hover:bg-opacity-10'
                                            }`}
                                    >
                                        <Icon className="h-4 w-4" />
                                        <span>{item.label}</span>
                                    </Link>
                                );
                            })}
                        </div>
                    </div>

                    <div className="flex items-center space-x-4">
                        <span className="text-white text-sm">
                            {user?.email} ({user?.role})
                        </span>
                        <button
                            onClick={handleLogout}
                            className="flex items-center space-x-1 bg-white bg-opacity-20 hover:bg-opacity-30 text-white px-3 py-2 rounded-md text-sm font-medium transition-colors"
                        >
                            <LogOut className="h-4 w-4" />
                            <span>Logout</span>
                        </button>
                    </div>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;