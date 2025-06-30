import React, { useState, useEffect } from 'react';
import { Search, Plus, Edit, Trash2, Plane, Clock } from 'lucide-react';
import api from '../../api/config';

const ManageFlights = () => {
    const [flights, setFlights] = useState([]);
    const [loading, setLoading] = useState(true);
    const [searchTerm, setSearchTerm] = useState('');
    const [showAddForm, setShowAddForm] = useState(false);

    useEffect(() => {
        fetchFlights();
    }, []);

    const fetchFlights = async () => {
        try {
            // Mock data for demo
            setFlights([
                {
                    id: 1,
                    flightNumber: 'SF001',
                    origin: 'New York',
                    destination: 'Los Angeles',
                    departureTime: '14:30',
                    arrivalTime: '17:45',
                    price: 299.99,
                    totalSeats: 180,
                    availableSeats: 45
                },
                {
                    id: 2,
                    flightNumber: 'SF002',
                    origin: 'Chicago',
                    destination: 'Miami',
                    departureTime: '17:45',
                    arrivalTime: '20:30',
                    price: 199.99,
                    totalSeats: 150,
                    availableSeats: 23
                },
                {
                    id: 3,
                    flightNumber: 'SF003',
                    origin: 'Seattle',
                    destination: 'Boston',
                    departureTime: '20:15',
                    arrivalTime: '04:30',
                    price: 349.99,
                    totalSeats: 200,
                    availableSeats: 67
                }
            ]);
        } catch (error) {
            console.error('Error fetching flights:', error);
        } finally {
            setLoading(false);
        }
    };

    const filteredFlights = flights.filter(flight => {
        const searchQuery = searchTerm.toLowerCase();
        return flight.flightNumber.toLowerCase().includes(searchQuery) ||
            flight.origin.toLowerCase().includes(searchQuery) ||
            flight.destination.toLowerCase().includes(searchQuery);
    });

    const getOccupancyColor = (available, total) => {
        const percentage = ((total - available) / total) * 100;
        if (percentage >= 80) return 'bg-red-100 text-red-800';
        if (percentage >= 60) return 'bg-yellow-100 text-yellow-800';
        return 'bg-green-100 text-green-800';
    };

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-7xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Manage Flights</h1>
                    <p className="mt-2 text-gray-600">Create and manage your flight schedules</p>
                </div>

                <div className="bg-white rounded-lg shadow-md">
                    <div className="p-6 border-b border-gray-200">
                        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between space-y-4 sm:space-y-0">
                            <div className="relative">
                                <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
                                <input
                                    type="text"
                                    placeholder="Search flights..."
                                    className="pl-10 pr-4 py-2 border border-gray-300 rounded-md focus:ring-green-500 focus:border-green-500"
                                    value={searchTerm}
                                    onChange={(e) => setSearchTerm(e.target.value)}
                                />
                            </div>
                            <button
                                onClick={() => setShowAddForm(true)}
                                className="flex items-center space-x-2 bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-md transition-colors"
                            >
                                <Plus className="h-4 w-4" />
                                <span>Add Flight</span>
                            </button>
                        </div>
                    </div>

                    <div className="overflow-x-auto">
                        <table className="min-w-full divide-y divide-gray-200">
                            <thead className="bg-gray-50">
                                <tr>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Flight
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Route
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Schedule
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Price
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Occupancy
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Actions
                                    </th>
                                </tr>
                            </thead>
                            <tbody className="bg-white divide-y divide-gray-200">
                                {loading ? (
                                    <tr>
                                        <td colSpan="6" className="px-6 py-4 text-center">
                                            <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-green-600 mx-auto"></div>
                                        </td>
                                    </tr>
                                ) : (
                                    filteredFlights.map((flight) => (
                                        <tr key={flight.id} className="hover:bg-gray-50">
                                            <td className="px-6 py-4 whitespace-nowrap">
                                                <div className="flex items-center">
                                                    <Plane className="h-5 w-5 text-gray-400 mr-3" />
                                                    <div>
                                                        <div className="text-sm font-medium text-gray-900">
                                                            {flight.flightNumber}
                                                        </div>
                                                        <div className="text-sm text-gray-500">
                                                            ID: {flight.id}
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                                                {flight.origin} → {flight.destination}
                                            </td>
                                            <td className="px-6 py-4 whitespace-nowrap">
                                                <div className="flex items-center text-sm text-gray-900">
                                                    <Clock className="h-4 w-4 text-gray-400 mr-1" />
                                                    <div>
                                                        <div>{flight.departureTime} - {flight.arrivalTime}</div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                                                ${flight.price}
                                            </td>
                                            <td className="px-6 py-4 whitespace-nowrap">
                                                <div className="flex items-center">
                                                    <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${getOccupancyColor(flight.availableSeats, flight.totalSeats)}`}>
                                                        {flight.totalSeats - flight.availableSeats}/{flight.totalSeats}
                                                    </span>
                                                </div>
                                            </td>
                                            <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                                <div className="flex space-x-2">
                                                    <button className="text-green-600 hover:text-green-900">
                                                        <Edit className="h-4 w-4" />
                                                    </button>
                                                    <button className="text-red-600 hover:text-red-900">
                                                        <Trash2 className="h-4 w-4" />
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                    ))
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ManageFlights;