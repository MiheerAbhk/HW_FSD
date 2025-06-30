import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { useAuth } from './context/AuthContext';
import Navbar from './components/Navbar';
import Login from './pages/Login';
import PrivateRoute from './routes/PrivateRoute';

// Admin Pages
import AdminDashboard from './pages/admin/AdminDashboard';
import ManageUsers from './pages/admin/ManageUsers';
import ManageRoutes from './pages/admin/ManageRoutes';
import ViewBookings from './pages/admin/ViewBookings';

// Flight Owner Pages
import FlightOwnerDashboard from './pages/flight-owner/FlightOwnerDashboard';
import ManageAirline from './pages/flight-owner/ManageAirline';
import ManageFlights from './pages/flight-owner/ManageFlights';
import OwnerBookings from './pages/flight-owner/OwnerBookings';

// Passenger Pages
import PassengerDashboard from './pages/passenger/PassengerDashboard';
import FlightSearch from './pages/passenger/FlightSearch';
import BookFlight from './pages/passenger/BookFlight';
import MyBookings from './pages/passenger/MyBookings';
import Payment from './pages/passenger/Payment';

function App() {
    const { user, loading } = useAuth();

    if (loading) {
        return (
            <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-50 to-indigo-100">
                <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50">
            {user && <Navbar />}
            <Routes>
                <Route
                    path="/login"
                    element={!user ? <Login /> : <Navigate to={getDashboardRoute(user.role)} />}
                />
                <Route
                    path="/"
                    element={user ? <Navigate to={getDashboardRoute(user.role)} /> : <Navigate to="/login" />}
                />

                {/* Admin Routes */}
                <Route path="/admin" element={<PrivateRoute allowedRoles={['Admin']} />}>
                    <Route path="dashboard" element={<AdminDashboard />} />
                    <Route path="users" element={<ManageUsers />} />
                    <Route path="routes" element={<ManageRoutes />} />
                    <Route path="bookings" element={<ViewBookings />} />
                </Route>

                {/* Flight Owner Routes */}
                <Route path="/flight-owner" element={<PrivateRoute allowedRoles={['FlightOwner']} />}>
                    <Route path="dashboard" element={<FlightOwnerDashboard />} />
                    <Route path="airline" element={<ManageAirline />} />
                    <Route path="flights" element={<ManageFlights />} />
                    <Route path="bookings" element={<OwnerBookings />} />
                </Route>

                {/* Passenger Routes */}
                <Route path="/passenger" element={<PrivateRoute allowedRoles={['Passenger']} />}>
                    <Route path="dashboard" element={<PassengerDashboard />} />
                    <Route path="search" element={<FlightSearch />} />
                    <Route path="book/:flightId" element={<BookFlight />} />
                    <Route path="bookings" element={<MyBookings />} />
                    <Route path="payment/:bookingId" element={<Payment />} />
                </Route>
            </Routes>
        </div>
    );
}

function getDashboardRoute(role) {
    switch (role) {
        case 'Admin':
            return '/admin/dashboard';
        case 'FlightOwner':
            return '/flight-owner/dashboard';
        case 'Passenger':
            return '/passenger/dashboard';
        default:
            return '/login';
    }
}

export default App;