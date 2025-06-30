import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { CreditCard, Lock, CheckCircle } from 'lucide-react';
import api from '../../api/config';

const Payment = () => {
    const { bookingId } = useParams();
    const navigate = useNavigate();
    const [booking, setBooking] = useState(null);
    const [paymentData, setPaymentData] = useState({
        cardNumber: '',
        expiryDate: '',
        cvv: '',
        cardholderName: ''
    });
    const [processing, setProcessing] = useState(false);
    const [success, setSuccess] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchBookingDetails();
    }, [bookingId]);

    const fetchBookingDetails = async () => {
        try {
            // Mock booking details
            setBooking({
                id: bookingId,
                flightNumber: 'SF001',
                route: 'New York → Los Angeles',
                date: '2024-01-25',
                time: '14:30 - 17:45',
                seat: '12A',
                passenger: 'John Doe',
                amount: 299.99
            });
        } catch (error) {
            console.error('Error fetching booking details:', error);
        } finally {
            setLoading(false);
        }
    };

    const handlePayment = async (e) => {
        e.preventDefault();
        setProcessing(true);

        try {
            // Mock payment processing
            await new Promise(resolve => setTimeout(resolve, 3000));
            setSuccess(true);

            // Redirect to bookings after success
            setTimeout(() => {
                navigate('/passenger/bookings');
            }, 2000);
        } catch (error) {
            console.error('Payment failed:', error);
        } finally {
            setProcessing(false);
        }
    };

    if (loading) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-purple-600"></div>
            </div>
        );
    }

    if (success) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="bg-white rounded-lg shadow-md p-8 text-center max-w-md">
                    <CheckCircle className="h-16 w-16 text-green-500 mx-auto mb-4" />
                    <h2 className="text-2xl font-bold text-gray-900 mb-2">Payment Successful!</h2>
                    <p className="text-gray-600 mb-4">
                        Your booking has been confirmed. You will receive a confirmation email shortly.
                    </p>
                    <p className="text-sm text-gray-500">
                        Redirecting to your bookings...
                    </p>
                </div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-4xl mx-auto">
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-gray-900">Complete Payment</h1>
                    <p className="mt-2 text-gray-600">Secure payment for your flight booking</p>
                </div>

                <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                    <div className="lg:col-span-2">
                        <div className="bg-white rounded-lg shadow-md p-6">
                            <div className="flex items-center space-x-2 mb-6">
                                <Lock className="h-5 w-5 text-green-500" />
                                <span className="text-sm text-gray-600">Secure SSL Encrypted Payment</span>
                            </div>

                            <form onSubmit={handlePayment} className="space-y-6">
                                <div>
                                    <label className="block text-sm font-medium text-gray-700 mb-2">
                                        Card Number
                                    </label>
                                    <div className="relative">
                                        <CreditCard className="absolute left-3 top-1/2 transform -translate-y-1/2 h-5 w-5 text-gray-400" />
                                        <input
                                            type="text"
                                            required
                                            placeholder="1234 5678 9012 3456"
                                            className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                            value={paymentData.cardNumber}
                                            onChange={(e) => setPaymentData({ ...paymentData, cardNumber: e.target.value })}
                                            maxLength="19"
                                        />
                                    </div>
                                </div>

                                <div className="grid grid-cols-2 gap-4">
                                    <div>
                                        <label className="block text-sm font-medium text-gray-700 mb-2">
                                            Expiry Date
                                        </label>
                                        <input
                                            type="text"
                                            required
                                            placeholder="MM/YY"
                                            className="w-full px-3 py-3 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                            value={paymentData.expiryDate}
                                            onChange={(e) => setPaymentData({ ...paymentData, expiryDate: e.target.value })}
                                            maxLength="5"
                                        />
                                    </div>
                                    <div>
                                        <label className="block text-sm font-medium text-gray-700 mb-2">
                                            CVV
                                        </label>
                                        <input
                                            type="text"
                                            required
                                            placeholder="123"
                                            className="w-full px-3 py-3 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                            value={paymentData.cvv}
                                            onChange={(e) => setPaymentData({ ...paymentData, cvv: e.target.value })}
                                            maxLength="4"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label className="block text-sm font-medium text-gray-700 mb-2">
                                        Cardholder Name
                                    </label>
                                    <input
                                        type="text"
                                        required
                                        placeholder="John Doe"
                                        className="w-full px-3 py-3 border border-gray-300 rounded-md focus:ring-purple-500 focus:border-purple-500"
                                        value={paymentData.cardholderName}
                                        onChange={(e) => setPaymentData({ ...paymentData, cardholderName: e.target.value })}
                                    />
                                </div>

                                <button
                                    type="submit"
                                    disabled={processing}
                                    className="w-full bg-purple-600 hover:bg-purple-700 text-white py-3 rounded-md transition-colors disabled:opacity-50 flex items-center justify-center space-x-2"
                                >
                                    {processing ? (
                                        <>
                                            <div className="animate-spin rounded-full h-5 w-5 border-b-2 border-white"></div>
                                            <span>Processing Payment...</span>
                                        </>
                                    ) : (
                                        <>
                                            <CreditCard className="h-5 w-5" />
                                            <span>Pay ${booking?.amount}</span>
                                        </>
                                    )}
                                </button>
                            </form>

                            <div className="mt-6 text-center text-sm text-gray-500">
                                <p>This is a demo payment form. No actual charges will be made.</p>
                            </div>
                        </div>
                    </div>

                    <div className="lg:col-span-1">
                        <div className="bg-white rounded-lg shadow-md p-6 sticky top-6">
                            <h3 className="text-lg font-semibold text-gray-900 mb-4">Booking Summary</h3>

                            {booking && (
                                <div className="space-y-3 mb-6">
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Flight</span>
                                        <span className="font-medium">{booking.flightNumber}</span>
                                    </div>
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Route</span>
                                        <span className="font-medium">{booking.route}</span>
                                    </div>
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Date</span>
                                        <span className="font-medium">{new Date(booking.date).toLocaleDateString()}</span>
                                    </div>
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Time</span>
                                        <span className="font-medium">{booking.time}</span>
                                    </div>
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Seat</span>
                                        <span className="font-medium">{booking.seat}</span>
                                    </div>
                                    <div className="flex justify-between">
                                        <span className="text-gray-600">Passenger</span>
                                        <span className="font-medium">{booking.passenger}</span>
                                    </div>
                                </div>
                            )}

                            <div className="border-t pt-4">
                                <div className="flex items-center justify-between text-lg font-bold">
                                    <span>Total Amount</span>
                                    <span>${booking?.amount}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Payment;