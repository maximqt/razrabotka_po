import hashlib
import itertools
import time
import threading
from queue import Queue

hashes_to_crack = [
    "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad",  # SHA-256
    "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b",  # SHA-256
    "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f",  # SHA-256
    "7a68f09bd992671bb3b19a5e70b7827e"  # MD5
]

def hash_password(password):
    md5_hash = hashlib.md5(password.encode()).hexdigest()
    sha256_hash = hashlib.sha256(password.encode()).hexdigest()
    return md5_hash, sha256_hash

def single_thread_crack():
    start_time = time.time()
    for password_tuple in itertools.product("abcdefghijklmnopqrstuvwxyz", repeat=5):
        password = ''.join(password_tuple)
        md5_hash, sha256_hash = hash_password(password)
        for h in hashes_to_crack:
            if md5_hash == h or sha256_hash == h:
                print(f"Пароль найден: {password}, Хэш: {h}")
    end_time = time.time()
    print(f"Время выполнения (однопоточный): {end_time - start_time:.2f} секунд")

def worker(queue):
    while True:
        password = queue.get()
        if password is None:
            break
        md5_hash, sha256_hash = hash_password(password)
        for h in hashes_to_crack:
            if md5_hash == h or sha256_hash == h:
                print(f"Пароль найден: {password}, Хэш: {h}")
        queue.task_done()

def multi_thread_crack(num_threads):
    queue = Queue()
    threads = []
    start_time = time.time()

    for _ in range(num_threads):
        thread = threading.Thread(target=worker, args=(queue,))
        thread.start()
        threads.append(thread)

    for password_tuple in itertools.product("abcdefghijklmnopqrstuvwxyz", repeat=5):
        password = ''.join(password_tuple)
        queue.put(password)

    queue.join()

    for _ in range(num_threads):
        queue.put(None)
    for thread in threads:
        thread.join()

    end_time = time.time()
    print(f"Время выполнения (многопоточный): {end_time - start_time:.2f} секунд")

if __name__ == "__main__":
    mode = input("Выберите режим (1 - однопоточный, 2 - многопоточный): ")
    if mode == "1":
        single_thread_crack()
    elif mode == "2":
        num_threads = int(input("Введите количество потоков: "))
        multi_thread_crack(num_threads)
    else:
        print("Неверный режим.")
