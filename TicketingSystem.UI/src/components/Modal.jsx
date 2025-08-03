import React from 'react'

export default function Modal({ isOpen, title, children, onClose }) {
  if (!isOpen) return null

  return (
    <div className="fixed inset-0 bg-black bg-opacity-30 dark:bg-opacity-60 flex justify-center items-center z-50">
      <div className="bg-white dark:bg-gray-800 dark:text-white rounded-lg shadow-lg p-6 w-full max-w-md relative">
        <h2 className="text-lg font-bold mb-4">{title}</h2>
        {children}
        <button onClick={onClose} className="absolute top-2 right-2 text-gray-500 dark:text-gray-300 hover:text-black dark:hover:text-white">
          ✕
        </button>
      </div>
    </div>
  )
}