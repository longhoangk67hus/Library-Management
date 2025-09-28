import React, { useEffect, useState } from "react";
import { getBooks } from "../services/api";

function BookList() {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    getBooks().then((res) => setBooks(res.data));
  }, []);

  return (
    <div>
      <h2>Danh sách sách</h2>
      <ul>
        {books.map((book) => (
          <li key={book.id}>{book.title} - {book.author}</li>
        ))}
      </ul>
    </div>
  );
}

export default BookList;
